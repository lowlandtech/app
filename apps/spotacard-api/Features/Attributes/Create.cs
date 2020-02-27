using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

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

            public Handler(GraphContext graph)
            {
                _graph = graph;
            }

            public async Task<AttributeEnvelope> Handle(Command request, CancellationToken cancellationToken)
            {
                var card = await _graph.Cards.SingleOrDefaultAsync(_card => _card.Id == request.CardId,
                    cancellationToken);
                if (card == null)
                    throw new RestException(HttpStatusCode.NotFound, new {Card = Constants.NOT_FOUND});

                var attribute = new CardAttribute
                {
                    Index = card.CardAttributes?.Count ?? 0,
                    Name = request.Attribute.Name,
                    Type = request.Attribute.Type,
                    Value = request.Attribute.Value,
                    Card = card,
                    CardId = card.Id
                };

                await _graph.Attributes.AddAsync(attribute, cancellationToken);
                await _graph.SaveChangesAsync(cancellationToken);

                return new AttributeEnvelope(attribute);
            }
        }
    }
}
