using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RazorLight;
using Spotacard.Core.Enums;
using Spotacard.Features.Templates.Enums;
using Spotacard.Features.Templates.Exceptions;
using Spotacard.Features.Templates.Types;
using Spotacard.Features.Templates.ViewModels;
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
                var templateCard = await _context.Cards.SingleOrDefaultAsync(c=> c.Id == message.Data.TemplateId, cancellationToken);

                if (templateCard == null || templateCard.Type != CardTypes.Template)
                {
                    throw new InvalidTemplateException(message.Data.TemplateId);
                }

                var appCard = await _context.Cards.SingleOrDefaultAsync(c => c.Id == message.Data.AppId, cancellationToken);

                if (appCard == null || appCard.Type != CardTypes.App)
                {
                    throw new InvalidAppException(message.Data.AppId);
                }

                var template = new TemplateVm(templateCard);
                var app = new AppVm(appCard);

                var project = new Project(_context);
                var engine1 = new RazorLightEngineBuilder()
                    .UseProject(project)
                    .UseMemoryCachingProvider()
                    .Build();
                var engine2 = new RazorLightEngineBuilder()
                    // required to have a default RazorLightProject type, but not required to create a template from string.
                    .UseEmbeddedResourcesProject(typeof(Program))
                    .UseMemoryCachingProvider()
                    .Build();


                var files = new List<FileData>();
                FileData file = null;

                switch (template.Type)
                {
                    case TemplateTypes.App:
                        var filename = await engine2.CompileRenderStringAsync(template.Id + "-filename",
                            template.FileName, appCard);
                        Func<Task<string>> action = () => engine1.CompileRenderStringAsync(template.Id + "-description",
                            templateCard.Description, appCard);
                        file = new FileData(filename, action);
                        files.Add(file);
                        break;
                    case TemplateTypes.Tables:
                        break;
                    case TemplateTypes.Packages:
                        break;
                    case TemplateTypes.Pages:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return new GenerateEnvelope(files);
            }
        }
    }
}
