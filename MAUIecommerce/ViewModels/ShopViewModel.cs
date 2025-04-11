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

        public ObservableCollection<Item?> Inventory
        {
            get
            {
                return new ObservableCollection<Item?>(_invSvc.Products);
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
        public void PurchaseItem() 
        {
            if (SelectedItem != null)
            {
                _cartSvc.AddOrUpdate(SelectedItem);
                NotifyPropertyChanged(nameof(Inventory));    
            }
            
        }
    }
}
