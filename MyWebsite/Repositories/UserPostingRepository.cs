using Microsoft.EntityFrameworkCore;
using MyWebsite.Data;
using MyWebsite.Models.MyInfor;
using MyWebsite.Repositories;

namespace MyWebsite.Repositories
{
    public class UserPostingRepository : IRepository<UserPosting>
    {
        private readonly ApplicationDbContext _db;
        
        public UserPostingRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(UserPosting entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var userPosting = await _db.UserPostings.FindAsync(id);
            if (userPosting != null)
            {
                _db.UserPostings.Remove(userPosting);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserPosting>> GetAllAsync()
        {
            return await _db.UserPostings.ToListAsync();
        }

        public async Task<UserPosting> GetByIdAsync(string id)
        {
            var userPosting = await _db.UserPostings.FindAsync(id);

            if (userPosting == null)
            {
                // Là một exception được ném khi key không tìm thấy trong dictionary
                throw new KeyNotFoundException();
            }
            return userPosting;

        }

        public async Task UpdateAsync(UserPosting entity)
        {
            _db.UserPostings.Update(entity);
            await _db.SaveChangesAsync();

        }
    }
}
