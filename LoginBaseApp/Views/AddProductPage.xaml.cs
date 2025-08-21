using LoginBaseApp.ViewModels;

namespace LoginBaseApp.Views;

public partial class AddProductPage : ContentPage
{
    public AddProductPage(AddProductViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
