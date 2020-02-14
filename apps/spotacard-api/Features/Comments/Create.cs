using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Domain;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Spotacard.Features.Comments
{
    public class Create
    {
        public class CommentData
        {
            public string Body { get; set; }
        }

        public class CommentDataValidator : AbstractValidator<CommentData>
        {
            public CommentDataValidator()
            {
                RuleFor(x => x.Body).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<CommentEnvelope>
        {
            public CommentData Comment { get; set; }

            public string Slug { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Comment).NotNull().SetValidator(new CommentDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, CommentEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUserAccessor _currentUserAccessor;

            public Handler(GraphContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task<CommentEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var card = await _context.Cards
                    .Include(x => x.Comments)
                    .FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (card == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Card = Constants.NOT_FOUND });
                }

                var author = await _context.Persons.FirstAsync(x => x.Username == _currentUserAccessor.GetCurrentUsername(), cancellationToken);

                var comment = new Comment()
                {
                    Author = author,
                    Body = message.Comment.Body,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _context.Comments.AddAsync(comment, cancellationToken);

                card.Comments.Add(comment);

                await _context.SaveChangesAsync(cancellationToken);

                return new CommentEnvelope(comment);
            }
        }
    }
}
