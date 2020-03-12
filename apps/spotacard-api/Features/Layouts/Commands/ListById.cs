using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Layouts.Types;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Layouts.Commands
{
    public class ListById
    {
        public class Query : IRequest<LayoutEnvelope>
        {
            public Query(Guid layoutId)
            {
                LayoutId = layoutId;
            }

            public Guid LayoutId { get; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.LayoutId).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, LayoutEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<LayoutEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var layout = await _context.Layouts
                    .FirstOrDefaultAsync(x => x.Id == message.LayoutId, cancellationToken);

                if (layout == null) throw new RestException(HttpStatusCode.NotFound, new { Layout = Constants.NOT_FOUND });
                return new LayoutEnvelope(layout);
            }
        }
    }
}
