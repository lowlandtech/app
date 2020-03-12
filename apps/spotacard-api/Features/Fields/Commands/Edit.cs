using FluentValidation;
using MediatR;
using Spotacard.Features.Fields.Types;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Fields.Commands
{
    public class Edit
    {
        public class FieldData
        {
            public string Name { get; set; }
        }

        public class Command : IRequest<FieldEnvelope>
        {
            public FieldData Field { get; set; }
            public Guid FieldId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Field).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, FieldEnvelope>
        {
            private readonly GraphContext _context;
            public Handler(GraphContext context)
            {
                _context = context;
            }
            public async Task<FieldEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new FieldBuilder(_context)
                    .UseEdit(message.Field, message.FieldId)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
