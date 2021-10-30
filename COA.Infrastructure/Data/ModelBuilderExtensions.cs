using COA.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COA.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData
                (
                    new User { Id = 1, CreatedAt = new DateTime(2021, 10, 30), Email = "flewfarfaster@gmail.com", FirstName = "Tufic Alejandro", LastName = "Jacobo Guerrero", Phone = 1138313792 },
                    new User { Id = 2, CreatedAt = new DateTime(2021, 10, 30), Email = "seed2@example.com", FirstName = "Seed 2 FirstName", LastName = "Seed 2 LastName", Phone = 1234567891 },
                    new User { Id = 3, CreatedAt = new DateTime(2021, 10, 30), Email = "seed3@example.com", FirstName = "Seed 3 FirstName", LastName = "Seed 3 LastName", Phone = 1234567891 },
                    new User { Id = 4, CreatedAt = new DateTime(2021, 10, 30), Email = "seed4@example.com", FirstName = "Seed 4 FirstName", LastName = "Seed 4 LastName", Phone = 1234567891 },
                    new User { Id = 5, CreatedAt = new DateTime(2021, 10, 30), Email = "seed5@example.com", FirstName = "Seed 5 FirstName", LastName = "Seed 5 LastName", Phone = 1234567891 }
                );
        }

    }
}
