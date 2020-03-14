using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Contents.Types;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Contents.Commands
{
    public class ListById
    {
        public class Query : IRequest<ContentEnvelope>
        {
            public Query(Guid contentId)
            {
                ContentId = contentId;
            }

            public Guid ContentId { get; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.ContentId).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, ContentEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<ContentEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var content = await _context.Contents
                    .FirstOrDefaultAsync(x => x.Id == message.ContentId, cancellationToken);

                if (content == null) throw new RestException(HttpStatusCode.NotFound, new { Content = Constants.NOT_FOUND });
                return new ContentEnvelope(content);
            }
        }
    }
}
