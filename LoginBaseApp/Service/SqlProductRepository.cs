using LoginBaseApp.Models;
using SQLite;

namespace LoginBaseApp.Service
{
    public class SqlProductRepository
    {
        private readonly Task<SQLiteAsyncConnection> _dbTask;
        public SqlProductRepository() => _dbTask = Database.GetAsync();

        public async Task<List<Product>> GetAllAsync()
        {
            var db = await _dbTask;
            return await db.Table<Product>()
                           .OrderByDescending(p => p.CreatedAt)
                           .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var db = await _dbTask;
            return await db.Table<Product>().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(Product p)
        {
            var db = await _dbTask;
            await db.InsertAsync(p);
            return true;
        }

        public async Task<bool> UpdateAsync(Product p)
        {
            var db = await _dbTask;
            await db.UpdateAsync(p);
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
