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
        Task<NCResult> Insert(UserInsertDTO userInsertDTO);
        Task<NCResult> Update(UserUpdateDTO userUpdateDTO, int id);
        Task<NCResult> Delete(int id);
    }
}
