using MediatR;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Attributes
{
  public class List
  {
    public class Query : IRequest<AttributesEnvelope>
    {
      public Query(Guid cardId)
      {
        CardId = cardId;
      }
      public Guid CardId { get; }
    }

    public class QueryHandler : IRequestHandler<Query, AttributesEnvelope>
    {
      private readonly GraphContext _graph;

      public QueryHandler(GraphContext graph)
      {
        _graph = graph;
      }

      public Task<AttributesEnvelope> Handle(Query request, CancellationToken cancellationToken)
      {
        throw new NotImplementedException();
      }
    }
  }
}
