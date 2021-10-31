using COA.Domain.Common;
using COA.Domain.DTOs.UserDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COA.Core.Interfaces
{
    public interface IUsersServices
    {
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> GetById(int id);
        Task<Result> Insert(UserInsertDTO userInsertDTO);
    }
}
