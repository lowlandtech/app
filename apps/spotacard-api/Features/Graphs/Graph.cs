using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Features.Attributes;
using Spotacard.Infrastructure;
using Create = Spotacard.Features.Cards.Create;

namespace Spotacard.Features.Graphs
{
    public class Graph : IGraph
    {
        private readonly GraphContext _context;
        private readonly IMediator _mediator;

        public Graph(GraphContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<string> GetCardAttribute(Guid cardId, string name)
        {
            var command = new ListByName.Query(cardId, name);
            var result = await _mediator.Send(command);
            return result.Attribute?.Value;
        }

        public async Task<Card> AddCard(ContractTypes type, string name, List<CardAttribute> attributes)
        {
            var command = new Create.Command
            {
                Card = new Create.CardData
                {
                    Title = name,
                    Body = name,
                    Description = name,
                    Attributes = attributes,
                    TagList = ""
                }
            };
            var result = await _mediator.Send(command);
            return result.Card;
        }
    }
}
