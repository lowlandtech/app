using System;
using FluentValidation;
using MediatR;
using Spotacard.Features.Widgets.Types;
using Spotacard.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Widgets.Commands
{
    public class Create
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

        public class WidgetDataValidator : AbstractValidator<WidgetData>
        {
            public WidgetDataValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<WidgetEnvelope>
        {
            public WidgetData Widget { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Widget).NotNull().SetValidator(new WidgetDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, WidgetEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public Handler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<WidgetEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new WidgetBuilder(_context)
                    .UseCreate(message.Widget)
                    .UseUser(_currentUser)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
