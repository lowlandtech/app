using AutoMapper;
using Spotacard.Domain;

namespace Spotacard.Features.Profiles
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, Profile>(MemberList.None);
        }
    }
}
