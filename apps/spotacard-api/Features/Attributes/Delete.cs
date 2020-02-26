using FluentValidation;
using MediatR;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Attributes
{
  public class Delete
  {
    public class Command : IRequest<AttributeEnvelope>
    {
      public Command(Guid id)
      {
        Id = id;
      }

      public Guid Id { get; private set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
      public CommandValidator()
      {
        RuleFor(x => x.Id).NotNull().NotEmpty();
      }
    }

    public class QueryHandler : IRequestHandler<Command, AttributeEnvelope>
    {
      private readonly ICurrentUserAccessor _currentUserAccessor;
      private readonly GraphContext _graph;

      public QueryHandler(GraphContext graph, ICurrentUserAccessor currentUserAccessor)
      {
        _graph = graph;
        _currentUserAccessor = currentUserAccessor;
      }

      public Task<AttributeEnvelope> Handle(Command request, CancellationToken cancellationToken)
      {
        throw new System.NotImplementedException();
      }
    }
  }
}
