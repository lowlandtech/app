using MediatR;
using Spotacard.Core.Contracts;
using Spotacard.Core.Types;
using Spotacard.Features.Users;
using System.Linq;

namespace Spotacard.Infrastructure
{
    public class GraphSeeder : Seeder
    {
        public GraphSeeder(GraphContext graph, IMediator mediator)
        {
            Activities.Add(new PersonSeeder(graph, mediator));
        }
    }

    public class PersonSeeder : ISeeder
    {
        private readonly GraphContext _graph;
        private readonly IMediator _mediator;

        public PersonSeeder(GraphContext graph, IMediator mediator)
        {
            _graph = graph;
            _mediator = mediator;
        }

        public void Execute()
        {
            if (_graph.Persons.SingleOrDefault(person => person.Username == "admin") != null) return;

            var command = new Create.Command
            {
                User = new Create.UserData
                {
                    Email = "admin@spotacard",
                    Password = "admin",
                    Username = "admin"
                }
            };
            _mediator.Send(command);
        }
    }
}
