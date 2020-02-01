using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Features.Cards;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                DefaultValidatorExtensions.NotNull(RuleFor(x => x.Slug)).NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Command, CardEnvelope>
        {
            private readonly ConduitContext _context;
            private readonly ICurrentUserAccessor _currentUserAccessor;

            public QueryHandler(ConduitContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task<CardEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var card = await _context.Cards.FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (card == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Card = Constants.NOT_FOUND });
                }

                var person = await _context.Persons.FirstOrDefaultAsync(x => x.Username == _currentUserAccessor.GetCurrentUsername(), cancellationToken);

                var favorite = await _context.CardFavorites.FirstOrDefaultAsync(x => x.CardId == card.Id && x.PersonId == person.PersonId, cancellationToken);

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
