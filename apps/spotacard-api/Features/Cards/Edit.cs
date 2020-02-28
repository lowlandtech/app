using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Cards
{
    public class Edit
    {
        public class CardData
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Body { get; set; }
            public string TagList { get; set; }
        }

        public class Command : IRequest<CardEnvelope>
        {
            public CardData Card { get; set; }
            public string Slug { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Card).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, CardEnvelope>
        {
            private readonly GraphContext _graph;
            public Handler(GraphContext graph)
            {
                _graph = graph;
            }
            public async Task<CardEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var card = await _graph.Cards
                    .Include(x => x.CardTags) // include also the card tags since they also need to be updated
                    .Where(x => x.Slug == message.Slug)
                    .FirstOrDefaultAsync(cancellationToken);

                if (card == null) throw new RestException(HttpStatusCode.NotFound, new {Card = Constants.NOT_FOUND});

                card.Description = message.Card.Description ?? card.Description;
                card.Body = message.Card.Body ?? card.Body;
                card.Title = message.Card.Title ?? card.Title;
                card.Slug = card.Title.GenerateSlug();

                // list of currently saved card tags for the given card
                var cardTagList = message.Card.TagList?.Split(",") ?? Enumerable.Empty<string>();

                var cardTagsToCreate = GetCardTagsToCreate(card, cardTagList, _graph);
                var cardTagsToDelete = GetCardTagsToDelete(card, cardTagList);

                if (_graph.ChangeTracker.Entries().First(x => x.Entity == card).State == EntityState.Modified
                    || cardTagsToCreate.Any() || cardTagsToDelete.Any())
                    card.UpdatedAt = DateTime.UtcNow;

                // add the new card tags
                await _graph.CardTags.AddRangeAsync(cardTagsToCreate, cancellationToken);
                // delete the tags that do not exist anymore
                _graph.CardTags.RemoveRange(cardTagsToDelete);

                await _graph.SaveChangesAsync(cancellationToken);

                return new CardEnvelope(await _graph.Cards.GetAllData()
                    .Where(x => x.Slug == card.Slug)
                    .FirstOrDefaultAsync(cancellationToken));
            }

            /// <summary>
            ///     get the list of Tags to be added
            /// </summary>
            /// <param name="cardTagList"></param>
            /// <returns></returns>
            private async Task<List<Tag>> GetTagsToCreate(IEnumerable<string> cardTagList)
            {
                var tagsToCreate = new List<Tag>();
                foreach (var tag in cardTagList)
                {
                    var t = await _graph.Tags.FindAsync(tag);
                    if (t == null)
                    {
                        t = new Tag
                        {
                            TagId = tag
                        };
                        tagsToCreate.Add(t);
                    }
                }

                return tagsToCreate;
            }

            /// <summary>
            ///     check which card tags need to be added
            /// </summary>
            private static List<CardTag> GetCardTagsToCreate(Card card, IEnumerable<string> cardTagList,
                GraphContext graph)
            {
                var cardTagsToCreate = new List<CardTag>();
                foreach (var tag in cardTagList)
                {
                    var tagData = graph.Tags.Find(tag) ?? new Tag {TagId = tag};

                    var at = card.CardTags.FirstOrDefault(t => t.TagId == tag);

                    if (at == null)
                    {
                        at = new CardTag
                        {
                            Card = card,
                            CardId = card.Id,
                            Tag = tagData,
                            TagId = tag
                        };
                        cardTagsToCreate.Add(at);
                    }
                }

                return cardTagsToCreate;
            }

            /// <summary>
            ///     check which card tags need to be deleted
            /// </summary>
            private static List<CardTag> GetCardTagsToDelete(Card card, IEnumerable<string> cardTagList)
            {
                var cardTagsToDelete = new List<CardTag>();
                foreach (var tag in card.CardTags)
                {
                    var at = cardTagList.FirstOrDefault(t => t == tag.TagId);
                    if (at == null) cardTagsToDelete.Add(tag);
                }

                return cardTagsToDelete;
            }
        }
    }
}
