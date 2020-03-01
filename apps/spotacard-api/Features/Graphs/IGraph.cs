using Spotacard.Core.Enums;
using Spotacard.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotacard.Features.Graphs
{
    public interface IGraph
    {
        Task<string> GetCardAttribute(Guid cardId, string name);
        Task<Card> AddCard(ContractTypes type, string name, List<CardAttribute> attributes);
    }
}
