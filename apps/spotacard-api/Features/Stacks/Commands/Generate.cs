using FluentValidation;
using MediatR;
using RazorLight;
using Spotacard.Core.Contracts;
using Spotacard.Features.Stacks.Types;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Stacks.Commands
{
    public class Generate
    {
        public class GenerateData
        {
            public Guid StackId { get; set; }
            public Guid AppId { get; set; }
        }

        public class GenerateDataValidator : AbstractValidator<GenerateData>
        {
            public GenerateDataValidator()
            {
                RuleFor(x => x.StackId)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Stack id must be supplied.");

                RuleFor(x => x.AppId)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Application id must be supplied");
            }
        }

        public class Command : IRequest<Unit>
        {
            public Command(Guid stackId, Guid appId)
            {
                Data = new GenerateData
                {
                    StackId = stackId,
                    AppId = appId,
                };
            }

            public GenerateData Data { get; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Data).NotNull().SetValidator(new GenerateDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;
            private readonly ISettings _settings;

            public Handler(GraphContext context, ICurrentUser currentUser, ISettings settings)
            {
                _context = context;
                _currentUser = currentUser;
                _settings = settings;
            }

            public async Task<Unit> Handle(Command message, CancellationToken cancellationToken)
            {
                var workspace = new StackWorkspace(new StackContext
                {
                    Root = _settings.Repositories.FullName,
                    Engine = new RazorLightEngineBuilder()
                        .UseEmbeddedResourcesProject(typeof(Program))
                        .UseMemoryCachingProvider()
                        .Build(),
                    App = _context.Apps.Find(message.Data.AppId),
                    Stack = _context.Stacks.Find(message.Data.StackId),
                });
                await workspace.BeforeExecute();
                await workspace.Execute();

                return Unit.Value;
            }
        }
    }
}
