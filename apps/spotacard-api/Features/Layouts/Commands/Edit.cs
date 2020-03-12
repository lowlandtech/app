using FluentValidation;
using MediatR;
using Spotacard.Features.Layouts.Types;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Layouts.Commands
{
    public class Edit
    {
        public class LayoutData
        {
            public string Name { get; set; }
            public string Packages { get; set; }
            public string Wiring { get; set; }
            public string CodeBehind { get; set; }
            public string Styling { get; set; }
            public string Markup { get; set; }
            public string Items { get; set; }
        }

        public class Command : IRequest<LayoutEnvelope>
        {
            public LayoutData Layout { get; set; }
            public Guid LayoutId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Layout).NotNull();
                RuleFor(x => x.LayoutId).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, LayoutEnvelope>
        {
            private readonly GraphContext _context;
            public Handler(GraphContext context)
            {
                _context = context;
            }
            public async Task<LayoutEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new LayoutBuilder(_context)
                    .UseEdit(message.Layout, message.LayoutId)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
