using FluentValidation;
using MediatR;
using Spotacard.Features.Apps.Types;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Apps.Commands
{
    public class Edit
    {
        public class AppData
        {
            public string Name { get; set; }
            public string Organization { get; set; }
            public string Prefix { get; set; }
            public string Namingspace { get; set; }
        }

        public class Command : IRequest<AppEnvelope>
        {
            public AppData App { get; set; }
            public Guid AppId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.App).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, AppEnvelope>
        {
            private readonly GraphContext _context;
            public Handler(GraphContext context)
            {
                _context = context;
            }
            public async Task<AppEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new AppBuilder(_context)
                    .UseEdit(message.App, message.AppId)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
