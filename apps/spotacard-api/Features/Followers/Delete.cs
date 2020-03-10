using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Profiles;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Followers
{
    public class Delete
    {
        public class Command : IRequest<ProfileEnvelope>
        {
            public Command(string username)
            {
                Username = username;
            }

            public string Username { get; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Username).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Command, ProfileEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;
            private readonly IProfileReader _profileReader;

            public QueryHandler(GraphContext context, ICurrentUser currentUser,
                IProfileReader profileReader)
            {
                _context = context;
                _currentUser = currentUser;
                _profileReader = profileReader;
            }

            public async Task<ProfileEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var target =
                    await _context.Persons.FirstOrDefaultAsync(x => x.Username == message.Username, cancellationToken);

                if (target == null) throw new RestException(HttpStatusCode.NotFound, new {User = Constants.NOT_FOUND});

                var observer =
                    await _context.Persons.FirstOrDefaultAsync(
                        x => x.Username == _currentUser.GetCurrentUsername(), cancellationToken);

                var followedPeople =
                    await _context.FollowedPeople.FirstOrDefaultAsync(
                        x => x.ObserverId == observer.Id && x.TargetId == target.Id, cancellationToken);

                if (followedPeople != null)
                {
                    _context.FollowedPeople.Remove(followedPeople);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return await _profileReader.ReadProfile(message.Username);
            }
        }
    }
}
