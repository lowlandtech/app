using Spotacard.Domain;
using Spotacard.Features.Cards.Commands;
using Spotacard.Features.Cards.Types;
using Spotacard.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Cards.Infrastructure
{
    internal class CardBuilder
    {
        private readonly GraphContext _context;
        private readonly Card _card = new Card();

        public CardBuilder(GraphContext context)
        {
            _context = context;
        }

        public CardBuilder UseCard(Create.CardData card)
        {
            _card.Title = card.Title;
            _card.Slug = card.Title.ToSlug(_context);
            _card.Type = card.Type;
            _card.Description = card.Description;
            _card.Body = card.Body;
            _card.CreatedAt = DateTime.UtcNow;
            _card.UpdatedAt = DateTime.UtcNow;

            _context.Cards.Add(_card);

            return this;
        }

        public CardBuilder UseTags(string tagList)
        {
            var tags = new List<Tag>();

            foreach (var tag in tagList?.Split(",") ?? Enumerable.Empty<string>())
            {
                var t = _context.Tags.Find(tag);
                if (t == null)
                {
                    t = new Tag
                    {
                        TagId = tag
                    };
                    _context.Tags.Add(t);
                    //save immediately for reuse
                     _context.SaveChanges();
                }

                tags.Add(t);
            }

            _context.CardTags.AddRange(tags.Select(x => new CardTag
            {
                Card = _card,
                Tag = x
            }));

            return this;
        }

        public CardBuilder UseUser(ICurrentUser currentUser)
        {
            _card.Author = _context.Persons
                .First(x =>
                        x.Username == currentUser.GetCurrentUsername());
            
            return this;
        }

        public CardBuilder UseAttributes(List<CardAttribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                attribute.CardId = _card.Id;
                attribute.Card = _card;
                _card.CardAttributes.Add(attribute);
                _context.Attributes.Add(attribute);
            }

            return this;
        }
        public async Task<CardEnvelope> BuildAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return new CardEnvelope(_card);
        }
    }
}
