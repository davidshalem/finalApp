using LoginBaseApp.ViewModels;

namespace LoginBaseApp.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfileViewModel vm) // מתקבל דרך DI
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
