using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
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
            public GenerateData Generation { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Generation).NotNull().SetValidator(new GenerateDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, GenerateEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUserAccessor _currentUserAccessor;

            public Handler(GraphContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task<GenerateEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var template = await _context.Cards.FindAsync(message.Generation.TemplateId, cancellationToken);

                if (template == null || template.Type != CardTypes.Template)
                {
                    throw new InvalidTemplateException(message.Generation.TemplateId);
                }

                var app = await _context.Cards.FindAsync(message.Generation.AppId, cancellationToken);

                if (app == null || app.Type != CardTypes.App)
                {
                    throw new InvalidAppException(message.Generation.AppId);
                }

                var files = new List<FileData>();

                return new GenerateEnvelope(files);
            }
        }
    }
}
