using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Contents.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid ContentId { get; }

            public Command(Guid contentId)
            {
                ContentId = contentId;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ContentId).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Command>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command message, CancellationToken cancellationToken)
            {
                var Content = await _context.Contents
                    .FirstOrDefaultAsync(x => x.Id == message.ContentId, cancellationToken);

                if (Content == null) throw new RestException(HttpStatusCode.NotFound, new { Content = Constants.NOT_FOUND });

                _context.Contents.Remove(Content);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
