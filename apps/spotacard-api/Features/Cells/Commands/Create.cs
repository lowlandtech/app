using System;
using FluentValidation;
using MediatR;
using Spotacard.Features.Cells.Types;
using Spotacard.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Cells.Commands
{
    public class Create
    {
        public class CellData
        {
            public string Name { get; set; }
            public string Area { get; set; }
        }

        public class CellDataValidator : AbstractValidator<CellData>
        {
            public CellDataValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
                RuleFor(x => x.Area).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<CellEnvelope>
        {
            public CellData Cell { get; set; }
            public Guid PageId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Cell).NotNull().SetValidator(new CellDataValidator());
                RuleFor(x => x.PageId).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, CellEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public Handler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<CellEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new CellBuilder(_context)
                    .UseCreate(message.Cell, message.PageId)
                    .UseUser(_currentUser)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
