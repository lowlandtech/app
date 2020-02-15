using Spotacard.Domain;
using Spotacard.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Spotacard.Features.Cards
{
  public class CardService
  {
    private readonly GraphContext _graph;

    public CardService(GraphContext graph)
    {
      _graph = graph;
    }

    public void Add(string name)
    {
      var node = new Card { Title = name };
      _graph.Cards.Add(node);
      _graph.SaveChanges();
    }

    public IEnumerable<Card> Find(string term)
    {
      return _graph.Cards
        .Where(b => b.Title.Contains(term))
        .OrderBy(b => b.Title)
        .ToList();
    }
  }
}
