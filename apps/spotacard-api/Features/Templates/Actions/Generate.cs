using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using RazorLight;
using Spotacard.Core.Enums;
using Spotacard.Features.Templates.Exceptions;
using Spotacard.Features.Templates.Types;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Templates.Actions
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

        public class Command : IRequest<GenerateEnvelope>
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

        public class Handler : IRequestHandler<Command, GenerateEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public Handler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<GenerateEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var template = await _context.Cards.FindAsync(message.Data.TemplateId, cancellationToken);

                if (template == null || template.Type != CardTypes.Template)
                {
                    throw new InvalidTemplateException(message.Data.TemplateId);
                }

                var app = await _context.Cards.FindAsync(message.Data.AppId, cancellationToken);

                if (app == null || app.Type != CardTypes.App)
                {
                    throw new InvalidAppException(message.Data.AppId);
                }

                var project = new Project(_context);
                var engine = new RazorLightEngineBuilder()
                    .UseProject(project)
                    .UseMemoryCachingProvider()
                    .Build();

                var files = new List<FileData>();

                return new GenerateEnvelope(files);
            }
        }
    }
}
