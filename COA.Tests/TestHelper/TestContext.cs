using COA.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COA.Tests.TestHelper
{
    public class TestContext
    {
        public AppDbContext GetTestContext(string testDb)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(testDb)
                .Options;

            var dbcontext = new AppDbContext(options);

            return dbcontext;
        }
    }
}
