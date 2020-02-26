using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;

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
            public Guid CardId { get; set; }
        }

        public class Command : IRequest<AttributeEnvelope>
        {
            public AttributeData Attribute { get; set; }
            public Guid Id { get; set; }
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

            public async Task<AttributeEnvelope> Handle(Command request, CancellationToken cancellationToken)
            {
                var attribute = await _graph.Attributes.FindAsync(request.Id, cancellationToken);
                attribute.Index = request.Attribute.Index;
                attribute.Name = request.Attribute.Name;
                attribute.Type = request.Attribute.Type;
                attribute.Value = request.Attribute.Value;
                attribute.CardId = request.Attribute.CardId;

                if (_graph.ChangeTracker.Entries().First(x => x.Entity == attribute).State == EntityState.Modified)
                    attribute.Card.UpdatedAt = DateTime.UtcNow;

                await _graph.SaveChangesAsync(cancellationToken);
                return new AttributeEnvelope(attribute);
            }
        }
    }
}
