using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using springecommerce.models;

namespace MAUIecommerce.ViewModels
{
    public class ProductViewModel
    {
        public string? Name 
        {
            get
            {
                return Model?.Name ?? string.Empty;
            }
            set
            {
                if(Model != null && Model.Name != value)
                {
                    Model.Name = value;
                }
            }
        }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

        public Product? Model { get; set; }

        public ProductViewModel()
        {
            Model = new Product();
        }

        public ProductViewModel(Product? model)
        {
            Model = model;  
        }



    }
}
