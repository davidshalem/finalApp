using System.Windows.Input;
using LoginBaseApp.Models;
using LoginBaseApp.Service;
using Microsoft.Maui.Controls;

namespace LoginBaseApp.ViewModels
{
    // מקבלים פרמטרים מהניווט דרך IQueryAttributable (בלי [QueryProperty])
    public class ProductEditViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly SqlProductRepository _repo;

        public ProductEditViewModel(SqlProductRepository repo)
        {
            _repo = repo;
            SaveCommand = new Command(async () => await SaveAsync(), CanSave);
        }

        // null = מצב הוספה; ערך = מצב עריכה
        private int? _productId;
        public int? ProductId
        {
            get => _productId;
            private set { _productId = value; OnPropertyChanged(); }
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
        private bool CanSave() => !string.IsNullOrWhiteSpace(Name) && Price >= 0;

        // נקרא אוטומטית בעת ניווט עם מילון פרמטרים
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("ProductId", out var raw))
            {
                // לא משתמשים ב־"int?" בתבנית! תומך ב-int, long, string
                if (raw is int i) ProductId = i;
                else if (raw is long l && l >= int.MinValue && l <= int.MaxValue) ProductId = (int)l;
                else if (raw is string s && int.TryParse(s, out var j)) ProductId = j;
                else ProductId = null;
            }

            _ = LoadAsync();
        }

        private async Task LoadAsync()
        {
            if (ProductId is null) return; // מצב הוספה

            var p = await _repo.GetByIdAsync(ProductId.Value);
            if (p is null) return;

            Name = p.Name;
            Price = p.Price;
        }

        private async Task SaveAsync()
        {
            IsBusy = true;

            if (ProductId is null)
            {
                // הוספה
                await _repo.AddAsync(new Product
                {
                    Name = Name.Trim(),
                    Price = Price
                });
            }
            else
            {
                // עריכה
                var entity = await _repo.GetByIdAsync(ProductId.Value);
                if (entity is not null)
                {
                    entity.Name = Name.Trim();
                    entity.Price = Price;
                    await _repo.UpdateAsync(entity);
                }
            }

            IsBusy = false;
            await Shell.Current.GoToAsync(".."); // חזרה לרשימה
        }
    }
}
