using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Apps.Types;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Apps.Commands
{
    public class ListById
    {
        public class Query : IRequest<AppEnvelope>
        {
            public Query(Guid appId)
            {
                AppId = appId;
            }

            public Guid AppId { get; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.AppId).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, AppEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<AppEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var app = await _context.Apps
                    .FirstOrDefaultAsync(x => x.Id == message.AppId, cancellationToken);

                if (app == null) throw new RestException(HttpStatusCode.NotFound, new { App = Constants.NOT_FOUND });
                return new AppEnvelope(app);
            }
        }
    }
}
