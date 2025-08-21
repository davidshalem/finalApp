using LoginBaseApp.ViewModels;

namespace LoginBaseApp.Views;

public partial class ProductEditPage : ContentPage
{
    public ProductEditPage(ProductEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
