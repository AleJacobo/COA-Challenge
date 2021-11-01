using COA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COA.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Insert(T entity);
        Task Delete(int id);
        Task Update(T entity);
        bool EntityExists(int id);
    }
}
