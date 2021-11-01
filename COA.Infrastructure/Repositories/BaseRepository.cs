using COA.Domain.Common;
using COA.Domain.Entities;
using COA.Infrastructure.Data;
using COA.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COA.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        #region Fields and Constructor
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _entity;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }
        #endregion

        public async Task<T> GetById(int id)
        {
            var request = await _entity.Where(x => x.IsDeleted == false && x.Id == id)
                .FirstOrDefaultAsync();

            return request;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            var response = await _entity.Where(x => x.IsDeleted == false).ToListAsync();
            return response;
        }
        public async Task<Result> Insert(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.IsDeleted = false;

            var response = await _entity.AddAsync(entity);
            return new Result().Success("Se ha agregado con exito!!");
        }
        public async Task<Result> Update(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            _context.Update(entity);

            return new Result().Success($"Se ha actualizado correctamente!!");
        }
        public async Task<Result> Delete(int id)
        {
            var entity = await _entity.FindAsync(id);

            entity.IsDeleted = true;
            entity.CreatedAt = DateTime.Now;

            _entity.Update(entity);
            return new Result().Success($"Se ha borrado correctamente");
        }
        public bool EntityExists(int id)
        {
            var request = _entity.Any(x => x.Id == id && x.IsDeleted == false);
            return request;
        }
    }
}
