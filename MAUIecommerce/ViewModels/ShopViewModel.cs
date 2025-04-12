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
        public Item? SelectedItem { get; set; } 
        public Item? SelectedCartItem { get; set; }
        public ObservableCollection<Item?> Inventory
        {
            get
            {
                return new ObservableCollection<Item?>(_invSvc.Products.Where(i => i?.Quantity > 0));
            }
        }

        public ObservableCollection<Item?> ShoppingCart
        {
            get
            {
                return new ObservableCollection<Item?>(_cartSvc.CartItems.Where(i => i?.Quantity > 0));
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
                var shouldrefresh = SelectedItem.Quantity >= 1; 
                var updateditem =_cartSvc.AddOrUpdate(SelectedItem);

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
                var shouldrefresh = SelectedCartItem.Quantity >= 1;
                var updateditem = _cartSvc.ReturnItem(SelectedCartItem);

                if (updateditem != null && shouldrefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));

                }
            }

        }
    }
}

