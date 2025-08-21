using System.Threading.Tasks;
using LoginBaseApp.Models;
using LoginBaseApp.Helper;

namespace LoginBaseApp.Service
{
    public class SqlLoginService : ILoginService
    {
        private readonly SQlUserRepository _users;

        public SqlLoginService(SQlUserRepository users)
        {
            _users = users;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            username = username.Trim();

            var user = await _users.GetUserByUsernameAsync(username);
            if (user is null) return null;

            var hash = PasswordHelper.Hash(password);
            return user.Password == hash ? user : null;
        }
    }
}
