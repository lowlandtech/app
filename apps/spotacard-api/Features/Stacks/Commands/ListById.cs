using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Stacks.Types;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Stacks.Commands
{
    public class ListById
    {
        public class Query : IRequest<StackEnvelope>
        {
            public Query(Guid stackId)
            {
                StackId = stackId;
            }

            public Guid StackId { get; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.StackId).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, StackEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<StackEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var stack = await _context.Stacks
                    .FirstOrDefaultAsync(x => x.Id == message.StackId, cancellationToken);

                if (stack == null) throw new RestException(HttpStatusCode.NotFound, new { Stack = Constants.NOT_FOUND });
                return new StackEnvelope(stack);
            }
        }
    }
}
