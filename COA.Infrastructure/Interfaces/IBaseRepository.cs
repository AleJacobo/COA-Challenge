using COA.Domain.Common;
using COA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
        Task<IEnumerable<T>> GetPageAsync(Expression<Func<T, object>> order, int limit, int page);
        Task<int> CountAsync();

    }
}
