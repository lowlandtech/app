using FluentValidation;
using MediatR;
using Spotacard.Features.Tables.Types;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Tables.Commands
{
    public class Edit
    {
        public class TableData
        {
            public string Name { get; set; }
        }

        public class Command : IRequest<TableEnvelope>
        {
            public TableData Table { get; set; }
            public Guid TableId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Table).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, TableEnvelope>
        {
            private readonly GraphContext _context;
            public Handler(GraphContext context)
            {
                _context = context;
            }
            public async Task<TableEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new TableBuilder(_context)
                    .UseEdit(message.Table, message.TableId)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
