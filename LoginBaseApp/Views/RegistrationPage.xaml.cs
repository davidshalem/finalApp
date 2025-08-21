using LoginBaseApp.ViewModels;

namespace LoginBaseApp.Views;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage(RegistrationPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
