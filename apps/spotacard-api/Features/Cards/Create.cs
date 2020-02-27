using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Cards
{
    public class Create
    {
        public class CardData
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public string Body { get; set; }

            public string TagList { get; set; }
        }

        public class CardDataValidator : AbstractValidator<CardData>
        {
            public CardDataValidator()
            {
                RuleFor(x => x.Title).NotNull().NotEmpty();
                RuleFor(x => x.Description).NotNull().NotEmpty();
                RuleFor(x => x.Body).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<CardEnvelope>
        {
            public CardData Card { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Card).NotNull().SetValidator(new CardDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, CardEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUserAccessor _currentUserAccessor;

            public Handler(GraphContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task<CardEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var author =
                    await _context.Persons.FirstAsync(x => x.Username == _currentUserAccessor.GetCurrentUsername(),
                        cancellationToken);
                var tags = new List<Tag>();
                foreach (var tag in message.Card.TagList?.Split(",") ?? Enumerable.Empty<string>())
                {
                    var t = await _context.Tags.FindAsync(tag);
                    if (t == null)
                    {
                        t = new Tag
                        {
                            TagId = tag
                        };
                        await _context.Tags.AddAsync(t, cancellationToken);
                        //save immediately for reuse
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    tags.Add(t);
                }

                var card = new Card
                {
                    Author = author,
                    Body = message.Card.Body,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Description = message.Card.Description,
                    Title = message.Card.Title,
                    Slug = message.Card.Title.GenerateSlug()
                };
                await _context.Cards.AddAsync(card, cancellationToken);

                await _context.CardTags.AddRangeAsync(tags.Select(x => new CardTag
                {
                    Card = card,
                    Tag = x
                }), cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return new CardEnvelope(card);
            }
        }
    }
}
