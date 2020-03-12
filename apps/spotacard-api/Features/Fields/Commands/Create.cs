using System;
using FluentValidation;
using MediatR;
using Spotacard.Features.Fields.Types;
using Spotacard.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Core.Enums;

namespace Spotacard.Features.Fields.Commands
{
    public class Create
    {
        public class FieldData
        {
            public string Name { get; set; }
            public FieldTypes Type { get; set; }
            public Guid? WidgetId { get; set; }
        }

        public class FieldDataValidator : AbstractValidator<FieldData>
        {
            public FieldDataValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<FieldEnvelope>
        {
            public FieldData Field { get; set; }
            public Guid TableId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Field).NotNull().SetValidator(new FieldDataValidator());
                RuleFor(x => x.TableId).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, FieldEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public Handler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<FieldEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new FieldBuilder(_context)
                    .UseCreate(message.Field, message.TableId)
                    .UseUser(_currentUser)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
