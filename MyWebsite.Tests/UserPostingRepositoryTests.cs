    using Microsoft.EntityFrameworkCore;
    using MyWebsite.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.InMemory;
    using MyWebsite.Repositories;
    using MyWebsite.Models.MyInfor;
    using NuGet.ContentModel;
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json.Linq;
    namespace MyWebsite.Tests
    {
        public class UserPostingRepositoryTests
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

            [Fact]
            public async Task AddAsync_UserPosting()
            {
                // db context
                var db = CreateDbContext();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                // Repository 
                var repository = new UserPostingRepository(db);

                // UserPosting
                var userPosting = new UserPosting
                {
                    UserPostingId = "1",
                    FirstName = "John",
                    LastName = "Doe",
                    Gender = true,
                    DOB = new DateOnly(1990, 1, 1),
                    HouseNumber = "123",
                    Address = "123 Main St",
                    AvatarPath = "avatar.jpg",
                    UserId = "1"
                };

                // Execute
                await repository.AddAsync(userPosting);
                await db.SaveChangesAsync();

                // Result
                var result = db.UserPostings.Find(userPosting.UserPostingId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Doe", result.LastName);
            }

            // Unit Test for loading by Id 
            [Fact]
            public async Task getByIdAsync_UserPosting()
            {
                // db context
                var db = CreateDbContext();

                // EnsureDeleted and EnsureCreated
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                // Repository
                var repository = new UserPostingRepository(db);

                // Create UserPosting
                var userPosting = new UserPosting
                {
                    UserPostingId = "1",
                    FirstName = "John",
                    LastName = "Doe",
                    Gender = true,
                    DOB = new DateOnly(1990, 1, 1),
                    HouseNumber = "123",
                    Address = "123 Main St",
                    AvatarPath = "avatar.jpg",
                    UserId = "1"
                };

                await repository.AddAsync(userPosting);
                await db.SaveChangesAsync();

                // Result
                var result = await repository.GetByIdAsync(userPosting.UserPostingId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("1", result.UserPostingId);
            }

            [Fact]
            public async Task GetByIdAsync_ThrowKeyNotFoundException()
            {
                // db context   
                var db = CreateDbContext();
                // Repository
                var repository = new UserPostingRepository(db);

                await Assert.ThrowsAsync<KeyNotFoundException>(() => repository.GetByIdAsync("9999"));
            }

            [Fact]
            public async Task GetAllAsync_UserPosting()
            {
                // db context
                var db = CreateDbContext();
                // Repository
                var repository = new UserPostingRepository(db);

                // Create UserPosting
                var userPosting = new UserPosting
                {
                    UserPostingId = "1",
                    FirstName = "John",
                    LastName = "Doe",
                    Gender = true,
                    DOB = new DateOnly(1990, 1, 1),
                    HouseNumber = "123",
                    Address = "123 Main St",
                    AvatarPath = "avatar.jpg",
                    UserId = "1"
                };

                var userPosting2 = new UserPosting
                {
                    UserPostingId = "2",
                    FirstName = "Jame",
                    LastName = "Coach",
                    Gender = true,
                    DOB = new DateOnly(1990, 1, 1),
                    HouseNumber = "123",
                    Address = "123 Main St",
                    AvatarPath = "avatar.jpg",
                    UserId = "2"
                };
                await db.UserPostings.AddRangeAsync(userPosting, userPosting2);
                await db.SaveChangesAsync();

                var result = await repository.GetAllAsync();

                Assert.NotNull(result);
                Assert.Equal(2, result.Count());
            }

            // Test for UpdateAsync
            [Fact]
            public async Task UpdateAsync_ShouldUpdateUserPosting()
            {
                // db context
                var db = CreateDbContext();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                // Repository
                var repository = new UserPostingRepository(db);

                // Create UserPosting
                var userPosting = new UserPosting
                {
                    UserPostingId = "1",
                    FirstName = "John",
                    LastName = "Doe",
                    Gender = true,
                    DOB = new DateOnly(1990, 1, 1),
                    HouseNumber = "123",
                    Address = "123 Main St",
                    AvatarPath = "avatar.jpg",
                    UserId = "1"
                };

                await db.UserPostings.AddAsync(userPosting);
                await db.SaveChangesAsync();

                userPosting.FirstName = "Hell";
                await repository.UpdateAsync(userPosting);

                var result = db.UserPostings.Find(userPosting.UserPostingId);

                Assert.NotNull(result);
                Assert.Equal("Hell", result.FirstName);
            }


            [Fact]
            public async Task DeleteAsync_ShouldDeleteUserPosting()
            {
                // db context
                var db = CreateDbContext();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                // Repository
                var repository = new UserPostingRepository(db);
                // Create UserPosting
                var userPosting = new UserPosting
                {
                    UserPostingId = "1",
                    FirstName = "John",
                    LastName = "Doe",
                    Gender = true,
                    DOB = new DateOnly(1990, 1, 1),
                    HouseNumber = "123",
                    Address = "123 Main St",
                    AvatarPath = "avatar.jpg",
                    UserId = "1"
                };
                await db.UserPostings.AddAsync(userPosting);
                await db.SaveChangesAsync();

                await repository.DeleteAsync(userPosting.UserPostingId);
                var reselt = db.UserPostings.Find(userPosting.UserPostingId);

                Assert.Null(reselt);

            }

        }
    }