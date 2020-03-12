using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Fields.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid FieldId { get; }

            public Command(Guid fieldId)
            {
                FieldId = fieldId;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.FieldId).NotNull().NotEmpty();
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
                var Field = await _context.Fields
                    .FirstOrDefaultAsync(x => x.Id == message.FieldId, cancellationToken);

                if (Field == null) throw new RestException(HttpStatusCode.NotFound, new { Field = Constants.NOT_FOUND });

                _context.Fields.Remove(Field);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
