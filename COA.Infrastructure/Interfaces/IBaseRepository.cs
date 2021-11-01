using COA.Domain.Common;
using COA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COA.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<Result> Insert(T entity);
        Task<Result> Delete(int id);
        Task<Result> Update(T entity);
        bool EntityExists(int id);
    }
}
