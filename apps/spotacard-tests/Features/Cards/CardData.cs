using Spotacard.Core;
using Spotacard.Domain;
using Spotacard.Infrastructure;
using System;
using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;

namespace Spotacard.Features.Cards
{
  public class CardData : IActivity
  {
    private readonly GraphContext _graph;

    public const string FirstItemId = "312658D1-8146-42E3-B57B-360427182811";
    public const string SecondItemId = "64C7E3F5-74F9-4540-9B12-BC7AFBCC7CE6";

    public static readonly Card FirstItem = new Card
    {
      Id = Guid.Parse(FirstItemId),
      Title = "Item 1",
      Type = ContractTypes.Todo
    };

    public static readonly Card SecondItem = new Card
    {
      Id = Guid.Parse(SecondItemId),
      Title = "Item 2",
      Type = ContractTypes.Todo
    };

    public CardData(GraphContext graph)
    {
      _graph = graph;
      _graph.Database.EnsureDeleted();
      _graph.Database.EnsureCreated();
    }

    public void Execute()
    {
      _graph.Cards.Add(FirstItem);
      _graph.Cards.Add(SecondItem);
      _graph.SaveChanges();
    }
  }
}
