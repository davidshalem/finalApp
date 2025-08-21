using LoginBaseApp.ViewModels;

namespace LoginBaseApp.Views;

public partial class ProductsPage : ContentPage
{
    public ProductsPage(ProductsPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        // רענון אוטומטי בכל הופעה של המסך
        Appearing += async (_, __) => await vm.LoadAsync();
    }
}
