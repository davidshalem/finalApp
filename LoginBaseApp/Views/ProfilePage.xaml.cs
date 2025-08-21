using LoginBaseApp.ViewModels;

namespace LoginBaseApp.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfileViewModel vm) // ����� ��� DI
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
