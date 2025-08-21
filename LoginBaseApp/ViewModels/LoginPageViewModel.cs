using LoginBaseApp.Helper;
using LoginBaseApp.Service;
using System.Windows.Input;
using LoginBaseApp.Views;
using Microsoft.Maui.Controls;

namespace LoginBaseApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly ILoginService _auth;
        private readonly IUserSession _session;

        public LoginPageViewModel(ILoginService auth, IUserSession session)
        {
            _auth = auth;
            _session = session;

            ShowPasswordCommand = new Command(TogglePasswordVisiblity);
            LoginCommand = new Command(async () => await LoginAsync(), CanLogin);

            ShowPasswordIcon = FontHelper.CLOSED_EYE_ICON;
            IsPassword = true;
        }

        private string? _userName;
        public string? UserName
        {
            get => _userName;
            set
            {
                if (SetProperty(ref _userName, value))
                    (LoginCommand as Command)?.ChangeCanExecute();
            }
        }

        private string? _password;
        public string? Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                    (LoginCommand as Command)?.ChangeCanExecute();
            }
        }

        public bool CanLogin() => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);

        public ICommand ShowPasswordCommand { get; }
        public ICommand LoginCommand { get; }

        private bool messageIsVisible;
        public bool MessageIsVisible { get => messageIsVisible; set => SetProperty(ref messageIsVisible, value); }

        private Color? messageColor;
        public Color? MessageColor { get => messageColor; set => SetProperty(ref messageColor, value); }

        private bool isPassword;
        public bool IsPassword { get => isPassword; set => SetProperty(ref isPassword, value); }

        private string? showPasswordIcon;
        public string? ShowPasswordIcon { get => showPasswordIcon; set => SetProperty(ref showPasswordIcon, value); }

        private string? loginMessage;
        public string? LoginMessage { get => loginMessage; set => SetProperty(ref loginMessage, value); }

        private void TogglePasswordVisiblity()
        {
            IsPassword = !IsPassword;
            ShowPasswordIcon = IsPassword ? FontHelper.CLOSED_EYE_ICON : FontHelper.OPEN_EYE_ICON;
        }

        private async Task LoginAsync()
        {
            IsBusy = true;
            MessageIsVisible = true;

            var user = await _auth.LoginAsync(UserName!, Password!);

            if (user != null)
            {
                _session.CurrentUser = user;

                LoginMessage = AppMessages.LoginMessage;
                MessageColor = Colors.Green;

                await Shell.Current.GoToAsync(nameof(ProfilePage));
            }
            else
            {
                LoginMessage = AppMessages.LoginErrorMessage;
                MessageColor = Colors.Red;
            }

            IsBusy = false;
        }
    }
}
