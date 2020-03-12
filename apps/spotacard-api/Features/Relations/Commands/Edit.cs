using FluentValidation;
using MediatR;
using Spotacard.Features.Relations.Types;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Relations.Commands
{
    public class Edit
    {
        public class RelationData
        {
            public string Name { get; set; }
            public Guid PkFieldId { get; set; }
            public string PkName { get; set; }
            public Guid FkFieldId { get; set; }
            public string FkName { get; set; }
        }

        public class Command : IRequest<RelationEnvelope>
        {
            public RelationData Relation { get; set; }
            public Guid RelationId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Relation).NotNull();
                RuleFor(x => x.RelationId).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, RelationEnvelope>
        {
            private readonly GraphContext _context;
            public Handler(GraphContext context)
            {
                _context = context;
            }
            public async Task<RelationEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new RelationBuilder(_context)
                    .UseEdit(message.Relation, message.RelationId)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
