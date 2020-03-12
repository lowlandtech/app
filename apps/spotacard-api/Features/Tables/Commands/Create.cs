using System;
using FluentValidation;
using MediatR;
using Spotacard.Features.Tables.Types;
using Spotacard.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Tables.Commands
{
    public class Create
    {
        public class TableData
        {
            public string Name { get; set; }
        }

        public class TableDataValidator : AbstractValidator<TableData>
        {
            public TableDataValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<TableEnvelope>
        {
            public TableData Table { get; set; }
            public Guid AppId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Table).NotNull().SetValidator(new TableDataValidator());
                RuleFor(x => x.AppId).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, TableEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public Handler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<TableEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new TableBuilder(_context)
                    .UseCreate(message.Table, message.AppId)
                    .UseUser(_currentUser)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
