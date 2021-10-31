using COA.Domain;
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
