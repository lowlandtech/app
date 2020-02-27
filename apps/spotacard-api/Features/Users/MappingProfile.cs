using AutoMapper;
using Spotacard.Domain;

namespace Spotacard.Features.Users
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, User>(MemberList.None);
        }
    }
}
