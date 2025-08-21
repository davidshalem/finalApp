using System.Threading.Tasks;
using LoginBaseApp.Models;

namespace LoginBaseApp.Service
{
    public interface ILoginService
    {
        Task<User?> LoginAsync(string username, string password);
    }
}
