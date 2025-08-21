using LoginBaseApp.Models;

namespace LoginBaseApp.Service
{
    public interface IUserSession
    {
        User? CurrentUser { get; set; }
    }
}
