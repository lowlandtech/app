using Spotacard.Domain;
using Spotacard.Features.Cards.Commands;
using Spotacard.Features.Cards.Types;
using Spotacard.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Cards.Infrastructure
{
    internal class CardBuilder
    {
        private readonly GraphContext _context;
        private bool _isEditable = false;
        private Card _card = new Card();
        private List<CardTag> _cardTagsToCreate;
        private List<CardTag> _cardTagsToDelete;

        public CardBuilder(GraphContext context)
        {
            _context = context;
        }

        public CardBuilder UseCreate(Create.CardData card)
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

        public CardBuilder UseEdit(Edit.CardData card, string slug)
        {
            _card = _context.Cards // include also the card tags since they also need to be updated
                .Include(x => x.CardTags)
                .FirstOrDefault(x => x.Slug == slug);

            if (_card == null) throw new RestException(HttpStatusCode.NotFound, new { Card = Constants.NOT_FOUND });

            _card.Title = card.Title;
            _card.Description = card.Description;
            _card.Body = card.Body;
            _card.UpdatedAt = DateTime.UtcNow;

            _isEditable = true;

            return this;
        }

        public CardBuilder UseTags(string tagList)
        {
            var tags = new List<Tag>();
            _cardTagsToCreate = new List<CardTag>();
            _cardTagsToDelete = new List<CardTag>();

            if (_card.TagList.Count == 0)
            {
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
            }
            else
            {
                // list of currently saved card tags for the given card
                var cardTagList = tagList?.Split(",") ?? Enumerable.Empty<string>();

                _cardTagsToCreate = _card.GetCardTagsToCreate(cardTagList, _context);
                _cardTagsToDelete = _card.GetCardTagsToDelete(cardTagList);

                // add the new card tags
                _context.CardTags.AddRange(_cardTagsToCreate);
                // delete the tags that do not exist anymore
                _context.CardTags.RemoveRange(_cardTagsToDelete);
            }

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
            if (attributes == null) return this;

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
            if (_isEditable)
            {
                if (_context.ChangeTracker.Entries().First(x => x.Entity == _card).State == EntityState.Modified
                    || _cardTagsToCreate.Any() || _cardTagsToDelete.Any())
                    _card.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new CardEnvelope(await _context.Cards.GetAllData()
                .Where(x => x.Slug == _card.Slug)
                .FirstOrDefaultAsync(cancellationToken));
        }
    }
}
