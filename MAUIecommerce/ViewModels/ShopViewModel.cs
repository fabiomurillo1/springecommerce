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
        private readonly productserviceproxy _invSvc = productserviceproxy.Current;
        private readonly shoppingcartservice _cartSvc = shoppingcartservice.Current;
        private string _cartQuery = "";

        public ItemViewModel? SelectedItem { get; set; }
        public ItemViewModel? SelectedCartItem { get; set; }

        public ObservableCollection<ItemViewModel?> Inventory =>
            new ObservableCollection<ItemViewModel?>(
                _invSvc.Products.Where(i => i?.Quantity > 0)
                .Select(m => new ItemViewModel(new Item(m)))
            );

        public ObservableCollection<ItemViewModel?> ShoppingCart =>
            new ObservableCollection<ItemViewModel?>(
                _cartSvc.CartItems.Where(i => i?.Quantity > 0)
                .Select(m => new ItemViewModel(new Item(m)))
            );

        public ObservableCollection<ItemViewModel?> FilteredCart =>
            new ObservableCollection<ItemViewModel?>(
                _cartSvc.CartItems
                    .Where(i => i.Quantity > 0 &&
                                i.Product?.Name?.ToLower().Contains(_cartQuery.ToLower()) == true)
                    .Select(i => new ItemViewModel(new Item(i)))
            );

        public string TotalDisplay =>
            $"Total: ${_cartSvc.CartItems.Sum(i => i.Quantity * (i.Product?.Price ?? 0)):0.00}";

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void RefreshUX()
        {
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
            NotifyPropertyChanged(nameof(FilteredCart));
            NotifyPropertyChanged(nameof(TotalDisplay));
        }

        public async void PurchaseItem()
        {
            if (SelectedItem != null)
            {
                var shouldrefresh = SelectedItem.Model.Quantity >= 1;
                var updateditem = await _cartSvc.AddOrUpdate(new Item(SelectedItem.Model));

                if (updateditem != null && shouldrefresh)
                {
                    RefreshUX();
                }
            }
        }

        public async void ReturnItem()
        {
            if (SelectedCartItem != null)
            {
                var shouldrefresh = SelectedCartItem.Model.Quantity >= 1;
                var updateditem = await _cartSvc.ReturnItem(new Item(SelectedCartItem.Model));

                if (updateditem != null && shouldrefresh)
                {
                    RefreshUX();
                }
            }
        }

        public void FilterCart(string query)
        {
            _cartQuery = query ?? "";
            NotifyPropertyChanged(nameof(FilteredCart));
        }

        public async Task CheckoutCart()
        {
            await _cartSvc.CheckoutAsync();
            RefreshUX();
        }
    }
}