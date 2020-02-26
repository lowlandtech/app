using FluentValidation;
using MediatR;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Attributes
{
  public class Create
  {
    public class AttributeData
    {
      public int Index { get; set; }
      public string Name { get; set; }
      public string Type { get; set; }
      public string Value { get; set; }
    }

    public class AttributeDataValidator : AbstractValidator<AttributeData>
    {
      public AttributeDataValidator()
      {
        RuleFor(x => x.Index).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Type).NotNull().NotEmpty();
        RuleFor(x => x.Value).NotNull().NotEmpty();
      }
    }

    public class Command : IRequest<AttributeEnvelope>
    {
      public AttributeData Attribute { get; set; }
      public Guid CardId { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
      public CommandValidator()
      {
        RuleFor(x => x.Attribute).NotNull().SetValidator(new AttributeDataValidator());
      }
    }

    public class Handler : IRequestHandler<Command, AttributeEnvelope>
    {
      private readonly GraphContext _graph;
      private readonly ICurrentUserAccessor _currentUserAccessor;

      public Handler(GraphContext graph, ICurrentUserAccessor currentUserAccessor)
      {
        _graph = graph;
        _currentUserAccessor = currentUserAccessor;
      }

      public Task<AttributeEnvelope> Handle(Command request, CancellationToken cancellationToken)
      {
        throw new NotImplementedException();
      }
    }
  }
}
