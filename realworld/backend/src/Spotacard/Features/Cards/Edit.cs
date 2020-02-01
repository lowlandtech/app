using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Domain;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Spotacard.Features.Cards
{
    public class Edit
    {
        public class ArticleData
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public string Body { get; set; }

            public string[] TagList { get; set; }
        }

        public class Command : IRequest<ArticleEnvelope>
        {
            public ArticleData Card { get; set; }
            public string Slug { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Card).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, ArticleEnvelope>
        {
            private readonly ConduitContext _context;

            public Handler(ConduitContext context)
            {
                _context = context;
            }

            public async Task<ArticleEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var card = await _context.Cards
                    .Include(x => x.ArticleTags)    // include also the card tags since they also need to be updated
                    .Where(x => x.Slug == message.Slug)
                    .FirstOrDefaultAsync(cancellationToken);

                if (card == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Card = Constants.NOT_FOUND });
                }

                card.Description = message.Card.Description ?? card.Description;
                card.Body = message.Card.Body ?? card.Body;
                card.Title = message.Card.Title ?? card.Title;
                card.Slug = card.Title.GenerateSlug();

                // list of currently saved card tags for the given card
                var articleTagList = (message.Card.TagList ?? Enumerable.Empty<string>());
                
                var articleTagsToCreate = GetArticleTagsToCreate(card, articleTagList);
                var articleTagsToDelete = GetArticleTagsToDelete(card, articleTagList);

                if (_context.ChangeTracker.Entries().First(x => x.Entity == card).State == EntityState.Modified
                    || articleTagsToCreate.Any() || articleTagsToDelete.Any())
                {
                    card.UpdatedAt = DateTime.UtcNow;
                }

                // add the new card tags
                await _context.ArticleTags.AddRangeAsync(articleTagsToCreate, cancellationToken);
                // delete the tags that do not exist anymore
                _context.ArticleTags.RemoveRange(articleTagsToDelete);

                await _context.SaveChangesAsync(cancellationToken);

                return new ArticleEnvelope(await _context.Cards.GetAllData()
                    .Where(x => x.Slug == card.Slug)
                    .FirstOrDefaultAsync(cancellationToken));
            }

            /// <summary>
            /// get the list of Tags to be added
            /// </summary>
            /// <param name="articleTagList"></param>
            /// <returns></returns>
            private async Task<List<Tag>> GetTagsToCreate(IEnumerable<string> articleTagList)
            {
                var tagsToCreate = new List<Tag>();
                foreach (var tag in articleTagList)
                {
                    var t = await _context.Tags.FindAsync(tag);
                    if (t == null)
                    {
                        t = new Tag()
                        {
                            TagId = tag
                        };
                        tagsToCreate.Add(t);
                    }
                }

                return tagsToCreate;
            }

            /// <summary>
            /// check which card tags need to be added
            /// </summary>
            static List<ArticleTag> GetArticleTagsToCreate(Card card, IEnumerable<string> articleTagList)
            {
                var articleTagsToCreate = new List<ArticleTag>();
                foreach (var tag in articleTagList)
                {
                    var at = card.ArticleTags.FirstOrDefault(t => t.TagId == tag);
                    if (at == null)
                    {
                        at = new ArticleTag()
                        {
                            Card = card,
                            CardId = card.Id,
                            Tag = new Tag() { TagId = tag },
                            TagId = tag
                        };
                        articleTagsToCreate.Add(at);
                    }
                }

                return articleTagsToCreate;
            }

            /// <summary>
            /// check which card tags need to be deleted
            /// </summary>
            static List<ArticleTag> GetArticleTagsToDelete(Card card, IEnumerable<string> articleTagList)
            {
                var articleTagsToDelete = new List<ArticleTag>();
                foreach (var tag in card.ArticleTags)
                {
                    var at = articleTagList.FirstOrDefault(t => t == tag.TagId);
                    if (at == null)
                    {
                        articleTagsToDelete.Add(tag);
                    }
                }

                return articleTagsToDelete;
            }
        }
    }
}
