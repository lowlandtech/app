using System;
using FluentValidation;
using MediatR;
using Spotacard.Features.Pages.Types;
using Spotacard.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Pages.Commands
{
    public class Create
    {
        public class PageData
        {
            public string Name { get; set; }
        }

        public class PageDataValidator : AbstractValidator<PageData>
        {
            public PageDataValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<PageEnvelope>
        {
            public PageData Page { get; set; }
            public Guid AppId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Page).NotNull().SetValidator(new PageDataValidator());
                RuleFor(x => x.AppId).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, PageEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public Handler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<PageEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                return await new PageBuilder(_context)
                    .UseCreate(message.Page, message.AppId)
                    .UseUser(_currentUser)
                    .BuildAsync(cancellationToken);
            }
        }
    }
}
