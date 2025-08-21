using LoginBaseApp.Views;

namespace LoginBaseApp;
public partial class AppShell : Shell
{
    private bool _navigated;

    public AppShell()
    {
        InitializeComponent();
        // רישום כל המסלולים (Routes) לניווט
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(ProductsPage), typeof(ProductsPage));
        Routing.RegisterRoute(nameof(ProductEditPage), typeof(ProductEditPage));
        Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));
        Routing.RegisterRoute(nameof(ProductsPage), typeof(ProductsPage));
        Routing.RegisterRoute(nameof(ProductEditPage), typeof(ProductEditPage));

    }


}
