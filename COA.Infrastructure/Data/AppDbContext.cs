using COA.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COA.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Al entrar en esta parte del builder, me aseguro que Email sea unico.
            builder.Entity<User>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
            builder.Seed();
        }
        public DbSet<User> Users { get; set; }  
        
    }
}
