using System.Windows.Input;
using LoginBaseApp.Service;
using LoginBaseApp.Views;
using Microsoft.Maui.Controls;

namespace LoginBaseApp.ViewModels
{
    public class RegistrationPageViewModel : ViewModelBase
    {
        private readonly IRegisterService _registerService;

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                if (SetProperty(ref _username, value))
                    (RegisterCommand as Command)?.ChangeCanExecute();
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                    (RegisterCommand as Command)?.ChangeCanExecute();
            }
        }

        public ICommand RegisterCommand { get; }

        public RegistrationPageViewModel(IRegisterService registerService)
        {
            _registerService = registerService;
            RegisterCommand = new Command(async () => await OnRegisterClickedAsync(), CanRegister);
        }

        private bool CanRegister() =>
            !string.IsNullOrWhiteSpace(Username) &&
            !string.IsNullOrWhiteSpace(Password);

        private async Task OnRegisterClickedAsync()
        {
            IsBusy = true;
            bool success = await _registerService.RegisterAsync(Username, Password);
            IsBusy = false;

            if (success)
            {
                await Application.Current.MainPage.DisplayAlert("הצלחה", "נרשמת בהצלחה. אפשר להתחבר.", "אישור");
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(
                    "שגיאה",
                    "שם המשתמש כבר קיים או שהפרטים לא תקינים",
                    "אישור"
                );
            }
        }
    }
}
