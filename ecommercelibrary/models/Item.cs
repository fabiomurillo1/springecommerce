using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ecommercelibrary.DTO;
using ecommercelibrary.services;
using springecommerce.models;

namespace ecommercelibrary.models
{
    public class Item
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; } 

        public string Display
        {
            get
            {
                return $"{Id}. {Product?.Name ?? string.Empty} - {Quantity} @ ${Price} each";
            }
        }

        public Item()
        {
            Product = new ProductDTO();
            Quantity = 0;
            Price = 0.0m;  
        }

        public Item(Item i)
        {
            Product = new ProductDTO(i.Product);
            Quantity = i.Quantity;
            Id = i.Id;
            Price = i.Price;  
        }

   
        public Item(ProductDTO productDTO)
        {
            Product = productDTO;
            Quantity = 0;
            Price = productDTO.Price;  
        }

        public override string ToString()
        {
            return $"{Product} Quantity: {Quantity} Price: ${Price}";
        }
    }
}
