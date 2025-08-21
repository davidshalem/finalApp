using System.Windows.Input;
using LoginBaseApp.Models;
using LoginBaseApp.Service;
using Microsoft.Maui.Controls;

namespace LoginBaseApp.ViewModels
{
    public class AddProductViewModel : ViewModelBase
    {
        private readonly SqlProductRepository _repo;

        public AddProductViewModel(SqlProductRepository repo)
        {
            _repo = repo;
            SaveCommand = new Command(async () => await SaveAsync(), CanSave);
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set { if (SetProperty(ref _name, value)) (SaveCommand as Command)?.ChangeCanExecute(); }
        }

        private double _price;
        public double Price
        {
            get => _price;
            set { if (SetProperty(ref _price, value)) (SaveCommand as Command)?.ChangeCanExecute(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private bool CanSave() => !string.IsNullOrWhiteSpace(Name) && Price >= 0;

        private async Task SaveAsync()
        {
            IsBusy = true;
            await _repo.AddAsync(new Product
            {
                Name = Name.Trim(),
                Price = Price
            });
            IsBusy = false;

            // חזרה לרשימת המוצרים
            await Shell.Current.GoToAsync("..");
        }
    }
}
