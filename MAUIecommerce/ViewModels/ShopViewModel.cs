using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ecommercelibrary.models;
using ecommercelibrary.services;

namespace MAUIecommerce.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {
        private productserviceproxy _invSvc = productserviceproxy.Current;
        private shoppingcartservice _cartSvc = shoppingcartservice.Current; 
        public ItemViewModel? SelectedItem { get; set; } 
        public ItemViewModel? SelectedCartItem { get; set; }
        public ObservableCollection<ItemViewModel?> Inventory
        {
            get
            {
                return new ObservableCollection<ItemViewModel?>(_invSvc.Products.Where(i => i?.Quantity > 0).Select(m => new ItemViewModel(m)));
            }
        }

        public ObservableCollection<ItemViewModel?> ShoppingCart
        {
            get
            {
                return new ObservableCollection<ItemViewModel>(_cartSvc.CartItems.Where(i => i?.Quantity > 0).Select(m => new ItemViewModel(m)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshUX()
        {
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
        }
        public void PurchaseItem() 
        {
            if (SelectedItem != null)
            {
                var shouldrefresh = SelectedItem.Model.Quantity >= 1; 
                var updateditem =_cartSvc.AddOrUpdate(SelectedItem.Model);

                if (updateditem != null && shouldrefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));

                }
            }
            
        }
        public void ReturnItem()
        {
            if (SelectedCartItem != null)
            {
                var shouldrefresh = SelectedCartItem.Model.Quantity >= 1;
                var updateditem = _cartSvc.ReturnItem(SelectedCartItem.Model);

                if (updateditem != null && shouldrefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));

                }
            }

        }
    }
}

