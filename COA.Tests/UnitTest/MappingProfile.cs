using AutoMapper;
using COA.Domain;
using COA.Domain.DTOs.UserDTOs;

namespace COA.Tests.Tests
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserInsertDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
        }
    }
}