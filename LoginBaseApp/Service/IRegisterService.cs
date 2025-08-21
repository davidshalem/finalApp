using System.Threading.Tasks;

namespace LoginBaseApp.Service
{
    public interface IRegisterService
    {
        Task<bool> RegisterAsync(string username, string password);
    }
}
