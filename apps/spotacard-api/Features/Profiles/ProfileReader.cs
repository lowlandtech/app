using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using Microsoft.EntityFrameworkCore;

namespace Spotacard.Features.Profiles
{
    public class ProfileReader : IProfileReader
    {
        private readonly GraphContext _context;
        private readonly ICurrentUserAccessor _currentUserAccessor;
        private readonly IMapper _mapper;

        public ProfileReader(GraphContext context, ICurrentUserAccessor currentUserAccessor, IMapper mapper)
        {
            _context = context;
            _currentUserAccessor = currentUserAccessor;
            _mapper = mapper;
        }

        public async Task<ProfileEnvelope> ReadProfile(string username)
        {
            var currentUserName = _currentUserAccessor.GetCurrentUsername();

            var person = await _context.Persons.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Username == username);

            if (person == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { User = Constants.NOT_FOUND });
            }
            var profile = _mapper.Map<Domain.Person, Profile>(person);

            if (currentUserName != null)
            {
                var currentPerson = await _context.Persons
                    .Include(x => x.Following)
                    .Include(x => x.Followers)
                    .FirstOrDefaultAsync(x => x.Username == currentUserName);

                if (currentPerson.Followers.Any(x => x.TargetId == person.Id))
                {
                    profile.IsFollowed = true;
                }
            }
            return new ProfileEnvelope(profile);
        }
    }
}