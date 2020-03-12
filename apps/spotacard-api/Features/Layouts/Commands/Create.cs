using System;
using FluentValidation;
using MediatR;
using Spotacard.Features.Layouts.Types;
using Spotacard.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Layouts.Commands
{
    public class Create
    {
        public class LayoutData
        {
            public string Name { get; set; }
            public string Packages { get; set; }
            public string Wiring { get; set; }
            public string CodeBehind { get; set; }
            public string Styling { get; set; }
            public string Markup { get; set; }
        }

        public class LayoutDataValidator : AbstractValidator<LayoutData>
        {
            public LayoutDataValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<LayoutEnvelope>
        {
            public LayoutData Layout { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Layout).NotNull().SetValidator(new LayoutDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, LayoutEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public Handler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<LayoutEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new LayoutBuilder(_context)
                    .UseCreate(message.Layout)
                    .UseUser(_currentUser)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
