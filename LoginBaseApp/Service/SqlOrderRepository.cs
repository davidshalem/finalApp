using LoginBaseApp.Models;
using SQLite;

namespace LoginBaseApp.Service
{
    public class SqlOrderRepository
    {
        private readonly Task<SQLiteAsyncConnection> _dbTask;
        public SqlOrderRepository() => _dbTask = Database.GetAsync();

        public async Task<List<Order>> GetForUserAsync(int userId)
        {
            var db = await _dbTask;
            return await db.Table<Order>()
                           .Where(o => o.UserId == userId)
                           .OrderByDescending(o => o.Date)
                           .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            var db = await _dbTask;
            return await db.Table<Order>().Where(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(Order o)
        {
            var db = await _dbTask;
            await db.InsertAsync(o);
            return true;
        }

        public async Task<bool> UpdateAsync(Order o)
        {
            var db = await _dbTask;
            await db.UpdateAsync(o);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var db = await _dbTask;
            var entity = await GetByIdAsync(id);
            if (entity is null) return false;
            await db.DeleteAsync(entity);
            return true;
        }
    }
}
