using FluentValidation;
using MediatR;
using RazorLight;
using Spotacard.Core.Contracts;
using Spotacard.Domain;
using Spotacard.Features.Generator.Types;
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
            public Guid TemplateId { get; set; }
            public Guid AppId { get; set; }
        }

        public class GenerateDataValidator : AbstractValidator<GenerateData>
        {
            public GenerateDataValidator()
            {
                RuleFor(x => x.TemplateId).NotNull().NotEmpty();
                RuleFor(x => x.AppId).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<GeneratorEnvelope>
        {
            public GenerateData Data { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Data).NotNull().SetValidator(new GenerateDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, GeneratorEnvelope>
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

            public async Task<GeneratorEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var repository = new StackRepository(new StackContext
                {
                    Root = _settings.Repositories.FullName,
                    Engine1 = new RazorLightEngineBuilder()
                        .UseProject(new Project(_context))
                        .UseMemoryCachingProvider()
                        .Build(),
                    Engine2 = new RazorLightEngineBuilder()
                        .UseEmbeddedResourcesProject(typeof(Program))
                        .UseMemoryCachingProvider()
                        .Build(),
                    App = new App(),
                    Stack = new Stack()
                });
                await repository.BeforeExecute();
                await repository.Execute();

                return new GeneratorEnvelope(repository.Items);
            }
        }
    }
}
