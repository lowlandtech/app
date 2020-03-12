using FluentValidation;
using MediatR;
using Spotacard.Features.Pages.Types;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Pages.Commands
{
    public class Edit
    {
        public class PageData
        {
            public string Name { get; set; }
        }

        public class Command : IRequest<PageEnvelope>
        {
            public PageData Page { get; set; }
            public Guid PageId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Page).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, PageEnvelope>
        {
            private readonly GraphContext _context;
            public Handler(GraphContext context)
            {
                _context = context;
            }
            public async Task<PageEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new PageBuilder(_context)
                    .UseEdit(message.Page, message.PageId)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
