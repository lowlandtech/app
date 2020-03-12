using System;
using FluentValidation;
using MediatR;
using Spotacard.Features.Relations.Types;
using Spotacard.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Relations.Commands
{
    public class Create
    {
        public class RelationData
        {
            public string Name { get; set; }
            public Guid PkFieldId { get; set; }
            public string PkName { get; set; }
            public Guid FkFieldId { get; set; }
            public string FkName { get; set; }
        }

        public class RelationDataValidator : AbstractValidator<RelationData>
        {
            public RelationDataValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
                RuleFor(x => x.PkFieldId).NotNull().NotEmpty();
                RuleFor(x => x.PkName).NotNull().NotEmpty();
                RuleFor(x => x.FkFieldId).NotNull().NotEmpty();
                RuleFor(x => x.FkName).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<RelationEnvelope>
        {
            public RelationData Relation { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Relation).NotNull().SetValidator(new RelationDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, RelationEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public Handler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<RelationEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new RelationBuilder(_context)
                    .UseCreate(message.Relation)
                    .UseUser(_currentUser)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
