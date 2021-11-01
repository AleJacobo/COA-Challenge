using COA.Domain.Common;
using COA.Domain.DTOs.UserDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COA.Core.Interfaces
{
    public interface IUsersServices<T> where T : class
    {
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> GetById(int id);
        Task<Result<T>> Insert(UserInsertDTO userInsertDTO);
        Task<Result<UserDTO>> Update(UserUpdateDTO userUpdateDTO, int id);
        Task<Result<UserDTO>> Delete(int id);
    }
}
