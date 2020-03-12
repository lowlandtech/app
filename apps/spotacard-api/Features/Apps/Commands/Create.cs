using FluentValidation;
using MediatR;
using Spotacard.Features.Apps.Types;
using Spotacard.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Apps.Commands
{
    public class Create
    {
        public class AppData
        {
            public string Name { get; set; }
            public string Organisation { get; set; }
            public string Prefix { get; set; }
            public string Namingspace { get; set; }
        }

        public class AppDataValidator : AbstractValidator<AppData>
        {
            public AppDataValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
                RuleFor(x => x.Organisation).NotNull().NotEmpty();
                RuleFor(x => x.Prefix).NotNull().NotEmpty();
                RuleFor(x => x.Namingspace).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<AppEnvelope>
        {
            public AppData App { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.App).NotNull().SetValidator(new AppDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, AppEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public Handler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<AppEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new AppBuilder(_context)
                    .UseCreate(message.App)
                    .UseTags(message.App.TagList)
                    .UseAttributes(message.App.Attributes)
                    .UseUser(_currentUser)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
