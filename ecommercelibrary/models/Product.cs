﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecommercelibrary.DTO;

namespace springecommerce.models
{
    public class Product
    {
        public int Id { get; set; } 
        public string? Name { get; set; }


        public string Display
        {
            get
            {
                return $"{Id}. {Name}";
            }
        }


        public Product() { 
        
            Name = string.Empty;
        }

        public Product(Product p)
        {
            Name = p.Name;
            Id = p.Id;
        }
         public override string ToString()
        {

            return Display ?? string.Empty;
        }

        public Product(ProductDTO p)
        {
            Name = p.Name;
            Id= p.Id;   
        }

    } 
    

}
