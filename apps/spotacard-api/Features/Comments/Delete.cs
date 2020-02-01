using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Spotacard.Features.Comments
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Command(string slug, int id)
            {
                Slug = slug;
                Id = id;
            }

            public string Slug { get; }
            public int Id { get; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Slug).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Command>
        {
            private readonly ConduitContext _context;

            public QueryHandler(ConduitContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command message, CancellationToken cancellationToken)
            {
                var card = await _context.Cards
                    .Include(x => x.Comments)
                    .FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (card == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Card = Constants.NOT_FOUND });
                }

                var comment = card.Comments.FirstOrDefault(x => x.CommentId == message.Id);
                if (comment == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Comment = Constants.NOT_FOUND });
                }

                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}