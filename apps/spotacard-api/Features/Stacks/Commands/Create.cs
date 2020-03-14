using System;
using FluentValidation;
using MediatR;
using Spotacard.Features.Stacks.Types;
using Spotacard.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Stacks.Commands
{
    public class Create
    {
        public class StackData
        {
            public string Name { get; set; }
            public Guid ContentId { get; set; }
        }

        public class StackDataValidator : AbstractValidator<StackData>
        {
            public StackDataValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
                RuleFor(x => x.ContentId).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<StackEnvelope>
        {
            public StackData Stack { get; set; }
            public Guid CardId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Stack).NotNull().SetValidator(new StackDataValidator());
                RuleFor(x => x.CardId).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, StackEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public Handler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<StackEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new StackBuilder(_context)
                    .UseCreate(message.Stack, message.CardId)
                    .UseUser(_currentUser)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
