using Microsoft.EntityFrameworkCore;
using MyWebsite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
namespace MyWebsite.Tests
{
    internal class UserPostingRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        // Cấu hình DbContext với InMemoryDatabase
        public UserPostingRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("UserPostingDb")
                .Options;
        }

        // Tạo Instance mới của DbContext
        private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);





    }
}
