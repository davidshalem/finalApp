using LoginBaseApp.Models;

namespace LoginBaseApp.Service
{
    public class UserSession : IUserSession
    {
        public User? CurrentUser { get; set; }
    }
}
