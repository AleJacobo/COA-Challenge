using COA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COA.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<User> UsersRepository { get; }

        void Dispose();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
