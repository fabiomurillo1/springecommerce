using System.ComponentModel;
using ecommercelibrary.models;
using ecommercelibrary.services;
using springecommerce.models;

namespace MAUIecommerce.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private Item? cachedModel;
        private decimal? _price;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string? Name
        {
            get => Model?.Product?.Name ?? string.Empty;
            set
            {
                if (Model != null && Model.Product?.Name != value)
                {
                    Model.Product.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public decimal? Price
        {
            get => Model?.Product?.Price;
            set
            {
                if (Model != null && value != Model.Product?.Price)
                {
                    Model.Product.Price = (decimal)value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public int? Quantity
        {
            get => Model?.Quantity;
            set
            {
                if (Model != null && value != null && Model.Quantity != value)
                {
                    Model.Quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        public Item? Model { get; set; }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddOrUpdate()
        {
            productserviceproxy.Current.AddOrUpdate(Model);
        }

        public void Undo()
        {
            productserviceproxy.Current.AddOrUpdate(cachedModel);
        }

        public ProductViewModel()
        {
            Model = new Item();
            cachedModel = null;
        }

        public ProductViewModel(Item? model)
        {
            Model = model ?? new Item();
            if (model != null)
            {
                cachedModel = new Item(model);
            }

            if (Model?.Product?.Id == 0)
            {
                Model.Product.Id = Model.Id;
            }
        }
    }
}



