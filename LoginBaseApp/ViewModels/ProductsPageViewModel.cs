using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using LoginBaseApp.Models;
using LoginBaseApp.Service;
using Microsoft.Maui.Controls;

namespace LoginBaseApp.ViewModels
{
    public class ProductsPageViewModel : ViewModelBase
    {
        private readonly SqlProductRepository _repo;

        public ObservableCollection<Product> Products { get; } = new();

        public ICommand LoadCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        // אופציונלי: public ICommand AddCommand { get; }

        public ProductsPageViewModel(SqlProductRepository repo)
        {
            _repo = repo;

            LoadCommand = new Command(async () => await LoadAsync());
            EditCommand = new Command<Product>(async (p) => await GoToEditAsync(p));
            DeleteCommand = new Command<Product>(async (p) => await DeleteAsync(p));
            // AddCommand    = new Command(async () => await GoToEditAsync(null));
        }

        public async Task LoadAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            Products.Clear();
            var list = await _repo.GetAllAsync();
            foreach (var p in list) Products.Add(p);

            IsBusy = false;
        }

        private async Task GoToEditAsync(Product? p)
        {
            var nav = new Dictionary<string, object>();
            if (p != null) nav["ProductId"] = p.Id; // עריכה; אם null — זה מצב הוספה
            await Shell.Current.GoToAsync(nameof(LoginBaseApp.Views.ProductEditPage), nav);
        }

        private async Task DeleteAsync(Product? p)
        {
            if (p is null) return;

            var ok = await Application.Current.MainPage.DisplayAlert(
                "מחיקה",
                $"למחוק את '{p.Name}'?",
                "כן",
                "לא"
            );
            if (!ok) return;

            await _repo.DeleteAsync(p.Id);
            await LoadAsync();
        }
    }
}
