using AutoMapper;
using COA.Domain.DTOs.UserDTOs;

namespace COA.Domain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserInsertDTO>().ReverseMap();
        }
    }
}
