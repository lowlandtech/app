using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Attributes
{
    public class Delete
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Command>
        {
            private readonly GraphContext _graph;

            public QueryHandler(GraphContext graph)
            {
                _graph = graph;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var attribute = await _graph.Attributes.FindAsync(request.Id);
                if (attribute == null)
                    throw new RestException(HttpStatusCode.NotFound, new {Attribute = Constants.NOT_FOUND});
                _graph.Attributes.Remove(attribute);
                await _graph.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
