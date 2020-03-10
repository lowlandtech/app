using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Features.Cards.Infrastructure;
using Spotacard.Features.Cards.Types;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Cards.Commands
{
    public class Create
    {
        public class CardData
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Body { get; set; }
            public string TagList { get; set; }
            public CardTypes Type { get; set; }
            public List<CardAttribute> Attributes { get; set; } = new List<CardAttribute>();
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
            private readonly ICurrentUser _currentUser;

            public Handler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<CardEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new CardBuilder(_context)
                    .UseCreate(message.Card)
                    .UseTags(message.Card.TagList)
                    .UseAttributes(message.Card.Attributes)
                    .UseUser(_currentUser)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
