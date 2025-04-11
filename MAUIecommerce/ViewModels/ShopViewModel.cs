using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecommercelibrary.models;
using ecommercelibrary.services;

namespace MAUIecommerce.ViewModels
{
    public class ShopViewModel
    {
        private productserviceproxy _invSvc = productserviceproxy.Current;

        public ObservableCollection<Item?> Inventory
        {
            get
            {
                return new ObservableCollection<Item?>(_invSvc.Products);
            }
        }
    }
}
