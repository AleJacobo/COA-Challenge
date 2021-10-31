using COA.Domain;
using COA.Infrastructure.Data;
using COA.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;

namespace COA.Infrastructure.Repositories
{
    public class UOW : IUnitOfWork
    {
        #region Constructor and Context
        private readonly AppDbContext _context;
        public UOW(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Repository and Implementation
        private readonly IBaseRepository<User> _usersRepository;
        public IBaseRepository<User> UsersRepository => _usersRepository ?? new BaseRepository<User>(_context);
        #endregion

        #region UnitOfWorkMethods
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
        #endregion
    }
}
