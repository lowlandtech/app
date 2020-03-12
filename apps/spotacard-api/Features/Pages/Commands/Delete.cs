using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Pages.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid PageId { get; }

            public Command(Guid pageId)
            {
                PageId = pageId;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.PageId).NotNull().NotEmpty();
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
                var Page = await _context.Pages
                    .FirstOrDefaultAsync(x => x.Id == message.PageId, cancellationToken);

                if (Page == null) throw new RestException(HttpStatusCode.NotFound, new { Page = Constants.NOT_FOUND });

                _context.Pages.Remove(Page);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
