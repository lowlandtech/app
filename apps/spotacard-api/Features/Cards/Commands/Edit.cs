using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Features.Cards.Infrastructure;
using Spotacard.Features.Cards.Types;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Cards.Commands
{
    public class Edit
    {
        public class CardData
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Body { get; set; }
            public string TagList { get; set; }
            public CardTypes Type { get; set; }
            public List<CardAttribute> Attributes { get; set; }
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
            private readonly GraphContext _context;
            public Handler(GraphContext context)
            {
                _context = context;
            }
            public async Task<CardEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new CardBuilder(_context)
                    .UseEdit(message.Card, message.Slug)
                    .UseTags(message.Card.TagList)
                    .UseAttributes(message.Card.Attributes)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
