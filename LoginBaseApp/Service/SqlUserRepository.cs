using SQLite;
using LoginBaseApp.Models;

namespace LoginBaseApp.Service
{
    public class SQlUserRepository
    {
        private readonly Task<SQLiteAsyncConnection> _dbTask;

        public SQlUserRepository()
        {
            // ניצול מחלקת Database כדי להבטיח חיבור יחיד וטבלה קיימת
            _dbTask = Database.GetAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var db = await _dbTask;
            return await db.Table<User>().ToListAsync();
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var db = await _dbTask;
            return await db.Table<User>()
                           .Where(u => u.Username == username)
                           .FirstOrDefaultAsync();
        }

        public async Task<bool> AddUserAsync(User user)
        {
            var existing = await GetUserByUsernameAsync(user.Username);
            if (existing != null) return false;

            var db = await _dbTask;
            await db.InsertAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            var db = await _dbTask;
            var user = await GetUserByUsernameAsync(username);
            if (user is null) return false;

            await db.DeleteAsync(user);
            return true;
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string passwordHash)
        {
            var db = await _dbTask;
            var user = await db.Table<User>()
                               .Where(u => u.Username == username && u.Password == passwordHash)
                               .FirstOrDefaultAsync();
            return user != null;
        }
    }
}
