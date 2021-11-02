using COA.Domain.DTOs.UserDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COA.Core.Interfaces
{
    public interface IUsersServices
    {
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> GetById(int id);
        Task Insert(UserInsertDTO userInsertDTO);
        Task Update(UserUpdateDTO userUpdateDTO, int id);
        Task Delete(int id);
    }
}
