using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

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
                var attribute = await _graph.Attributes
                    .FirstOrDefaultAsync(_attribute => _attribute.Id == request.Id, cancellationToken);
                if (attribute == null)
                    throw new RestException(HttpStatusCode.NotFound, new {Attribute = Constants.NOT_FOUND});

                var card = await _graph.Cards.SingleOrDefaultAsync(_card => _card.Id == request.Attribute.CardId,
                    cancellationToken);
                if (card == null)
                    throw new RestException(HttpStatusCode.NotFound, new {Card = Constants.NOT_FOUND});

                attribute.Index = request.Attribute.Index;
                attribute.Name = request.Attribute.Name;
                attribute.Type = request.Attribute.Type;
                attribute.Value = request.Attribute.Value;
                attribute.CardId = request.Attribute.CardId;
                attribute.Card = card;

                if (_graph.ChangeTracker.Entries().First(x => x.Entity == attribute).State == EntityState.Modified)
                    attribute.Card.UpdatedAt = DateTime.UtcNow;

                await _graph.SaveChangesAsync(cancellationToken);
                return new AttributeEnvelope(attribute);
            }
        }
    }
}
