using FluentValidation;
using MediatR;
using Spotacard.Features.Stacks.Types;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Stacks.Commands
{
    public class Edit
    {
        public class StackData
        {
            public string Name { get; set; }
            public Guid ContentId { get; set; }
        }

        public class Command : IRequest<StackEnvelope>
        {
            public StackData Stack { get; set; }
            public Guid StackId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Stack).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, StackEnvelope>
        {
            private readonly GraphContext _context;
            public Handler(GraphContext context)
            {
                _context = context;
            }
            public async Task<StackEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new StackBuilder(_context)
                    .UseEdit(message.Stack, message.StackId)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
