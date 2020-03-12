using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Pages.Types;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Pages.Commands
{
    public class ListById
    {
        public class Query : IRequest<PageEnvelope>
        {
            public Query(Guid pageId)
            {
                PageId = pageId;
            }

            public Guid PageId { get; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.PageId).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, PageEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<PageEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var page = await _context.Pages
                    .FirstOrDefaultAsync(x => x.Id == message.PageId, cancellationToken);

                if (page == null) throw new RestException(HttpStatusCode.NotFound, new { Page = Constants.NOT_FOUND });
                return new PageEnvelope(page);
            }
        }
    }
}
