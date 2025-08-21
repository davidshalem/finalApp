using SQLite;
using Microsoft.Maui.Storage;

namespace LoginBaseApp.Service
{
    public static class Database
    {
        private static SQLiteAsyncConnection? _conn;
        private static readonly SemaphoreSlim _mutex = new(1, 1);

        public static async Task<SQLiteAsyncConnection> GetAsync()
        {
            if (_conn != null) return _conn;

            await _mutex.WaitAsync();
            try
            {
                if (_conn == null)
                {
                    var path = Path.Combine(FileSystem.AppDataDirectory, "app.db3");
                    _conn = new SQLiteAsyncConnection(path,
                        SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

                    await _conn.CreateTableAsync<LoginBaseApp.Models.User>();
                    await _conn.CreateTableAsync<LoginBaseApp.Models.Product>();
                    await _conn.CreateTableAsync<LoginBaseApp.Models.Order>();
                }
            }
            finally { _mutex.Release(); }

            return _conn!;
        }
    }
}
