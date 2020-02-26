using FluentValidation;
using MediatR;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Attributes
{
  public class Edit
  {
    public class AttributeData
    {
      public int Index { get; set; }
      public string Name { get; set; }
      public string Type { get; set; }
      public string Value { get; set; }
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
        RuleFor(x => x.Attribute).NotNull();
      }
    }

    public class Handler : IRequestHandler<Command, AttributeEnvelope>
    {
      private readonly GraphContext _graph;

      public Handler(GraphContext graph)
      {
        _graph = graph;
      }

      public Task<AttributeEnvelope> Handle(Command request, CancellationToken cancellationToken)
      {
        throw new NotImplementedException();
      }
    }
  }
}
