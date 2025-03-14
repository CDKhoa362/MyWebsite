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

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserPosting>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserPosting> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(UserPosting entity)
        {
            _db.UserPostings.Update(entity);
            await _db.SaveChangesAsync();

        }
    }
}
