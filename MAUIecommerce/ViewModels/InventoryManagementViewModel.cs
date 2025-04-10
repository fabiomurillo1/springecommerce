using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecommercelibrary.services;
using springecommerce.models;

namespace MAUIecommerce.ViewModels
{
    public class InventoryManagementViewModel
    {
        public Product? SelectedProduct { get; set; }
        private productserviceproxy _svc = productserviceproxy.Current;
        public List<Product?> Products
        {
            get
            {
                return _svc.Products;
            }
        }

        public Product? Delete()
        {
            return _svc.Delete(SelectedProduct?.Id ?? 0);
        }
    }
}
