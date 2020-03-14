using FluentValidation;
using MediatR;
using Spotacard.Features.Contents.Types;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Contents.Commands
{
    public class Create
    {
        public class ContentData
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public string Data { get; set; }
            public string Example { get; set; }
        }

        public class ContentDataValidator : AbstractValidator<ContentData>
        {
            public ContentDataValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
                RuleFor(x => x.Text).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<ContentEnvelope>
        {
            public ContentData Content { get; set; }
            public Guid CardId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Content).NotNull().SetValidator(new ContentDataValidator());
                RuleFor(x => x.CardId).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, ContentEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public Handler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<ContentEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new ContentBuilder(_context)
                    .UseCreate(message.Content, message.CardId)
                    .UseUser(_currentUser)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
