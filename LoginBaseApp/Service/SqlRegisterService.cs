using LoginBaseApp.Models;
using LoginBaseApp.Helper;
using System.Threading.Tasks;

namespace LoginBaseApp.Service
{
    public class SqlRegisterService : IRegisterService
    {
        private readonly SQlUserRepository _repo;

        public SqlRegisterService(SQlUserRepository repo) => _repo = repo;

        public async Task<bool> RegisterAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            username = username.Trim();

            var user = new User
            {
                Username = username,
                Password = PasswordHelper.Hash(password)
            };

            return await _repo.AddUserAsync(user);
        }
    }
}
