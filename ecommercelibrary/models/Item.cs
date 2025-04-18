﻿using System;
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

        public int? Quantity {  get; set; }

          
        public override string ToString()
        {
            return $"{Product} Quantity:{Quantity}";
        }
        public string Display 
        {
            get
            {
                return $"{Product?.Display ?? string.Empty} {Quantity}";
            }
        }

        public Item() 
        {
            Product = new ProductDTO();
            Quantity = 0;
           ;
        }

        
        public Item(Item i)
        {
            Product = new ProductDTO(i.Product);
            Quantity = i.Quantity;
            Id = i.Id;
        
        }
    }
}
