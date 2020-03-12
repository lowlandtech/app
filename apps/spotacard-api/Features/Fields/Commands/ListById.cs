using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Fields.Types;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Fields.Commands
{
    public class ListById
    {
        public class Query : IRequest<FieldEnvelope>
        {
            public Query(Guid fieldId)
            {
                FieldId = fieldId;
            }

            public Guid FieldId { get; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.FieldId).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, FieldEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<FieldEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var field = await _context.Fields
                    .FirstOrDefaultAsync(x => x.Id == message.FieldId, cancellationToken);

                if (field == null) throw new RestException(HttpStatusCode.NotFound, new { Field = Constants.NOT_FOUND });
                return new FieldEnvelope(field);
            }
        }
    }
}
