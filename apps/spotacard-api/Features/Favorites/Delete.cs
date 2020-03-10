using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Cards;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Favorites
{
    public class Delete
    {
        public class Command : IRequest<CardEnvelope>
        {
            public Command(string slug)
            {
                Slug = slug;
            }

            public string Slug { get; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Slug).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Command, CardEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public QueryHandler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<CardEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var card = await _context.Cards.FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (card == null) throw new RestException(HttpStatusCode.NotFound, new {Card = Constants.NOT_FOUND});

                var person =
                    await _context.Persons.FirstOrDefaultAsync(
                        x => x.Username == _currentUser.GetCurrentUsername(), cancellationToken);

                var favorite =
                    await _context.CardFavorites.FirstOrDefaultAsync(
                        x => x.CardId == card.Id && x.PersonId == person.Id, cancellationToken);

                if (favorite != null)
                {
                    _context.CardFavorites.Remove(favorite);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return new CardEnvelope(await _context.Cards.GetAllData()
                    .FirstOrDefaultAsync(x => x.Id == card.Id, cancellationToken));
            }
        }
    }
}
