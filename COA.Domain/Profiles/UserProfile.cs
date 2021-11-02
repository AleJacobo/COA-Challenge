using AutoMapper;
using COA.Domain.DTOs.UserDTOs;

namespace COA.Domain.Profiles
{
    /// <summary>
    /// Perfil de User de Automapper
    /// </summary>
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserInsertDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
        }
    }
}
