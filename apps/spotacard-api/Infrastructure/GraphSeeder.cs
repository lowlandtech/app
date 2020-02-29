using MediatR;
using Spotacard.Core.Contracts;
using Spotacard.Core.Types;
using Spotacard.Features.Users;
using System.Linq;

namespace Spotacard.Infrastructure
{
    public class GraphSeeder : Seeder
    {
        public GraphSeeder(GraphContext context, IMediator mediator)
        {
            Activities.Add(new PersonSeeder(context, mediator));
        }
    }

    public class PersonSeeder : ISeeder
    {
        private readonly GraphContext _context;
        private readonly IMediator _mediator;

        public PersonSeeder(GraphContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public void Execute()
        {
            if (_context.Persons.SingleOrDefault(person => person.Username == "admin") != null) return;

            var command = new Create.Command
            {
                User = new Create.UserData
                {
                    Email = "admin@spotacard.com",
                    Password = "admin",
                    Username = "admin"
                }
            };
            _mediator.Send(command);
        }
    }
}
