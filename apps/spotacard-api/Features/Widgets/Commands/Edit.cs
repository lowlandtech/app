using FluentValidation;
using MediatR;
using Spotacard.Features.Widgets.Types;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Widgets.Commands
{
    public class Edit
    {
        public class WidgetData
        {
            public string Name { get; set; }
            public string Packages { get; set; }
            public string Wiring { get; set; }
            public string CodeBehind { get; set; }
            public string Styling { get; set; }
            public string Markup { get; set; }
        }

        public class Command : IRequest<WidgetEnvelope>
        {
            public WidgetData Widget { get; set; }
            public Guid WidgetId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Widget).NotNull();
                RuleFor(x => x.WidgetId).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, WidgetEnvelope>
        {
            private readonly GraphContext _context;
            public Handler(GraphContext context)
            {
                _context = context;
            }
            public async Task<WidgetEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new WidgetBuilder(_context)
                    .UseEdit(message.Widget, message.WidgetId)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
