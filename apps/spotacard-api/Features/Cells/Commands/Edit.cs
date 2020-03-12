using FluentValidation;
using MediatR;
using Spotacard.Features.Cells.Types;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Cells.Commands
{
    public class Edit
    {
        public class CellData
        {
            public string Name { get; set; }
            public string Area { get; set; }
        }

        public class Command : IRequest<CellEnvelope>
        {
            public CellData Cell { get; set; }
            public Guid CellId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Cell).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, CellEnvelope>
        {
            private readonly GraphContext _context;
            public Handler(GraphContext context)
            {
                _context = context;
            }
            public async Task<CellEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new CellBuilder(_context)
                    .UseEdit(message.Cell, message.CellId)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
