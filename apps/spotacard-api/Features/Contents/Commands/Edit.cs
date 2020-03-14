using FluentValidation;
using MediatR;
using Spotacard.Features.Contents.Types;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Contents.Commands
{
    public class Edit
    {
        public class ContentData
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public string Data { get; set; }
            public string Example { get; set; } public int Index { get; set; }
        }

        public class Command : IRequest<ContentEnvelope>
        {
            public ContentData Content { get; set; }
            public Guid ContentId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Content).NotNull();
                RuleFor(x => x.ContentId).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, ContentEnvelope>
        {
            private readonly GraphContext _context;
            public Handler(GraphContext context)
            {
                _context = context;
            }
            public async Task<ContentEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new ContentBuilder(_context)
                    .UseEdit(message.Content, message.ContentId)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
