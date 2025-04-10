using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace springecommerce.models
{
    public class Product
    {
        public int Id { get; set; } 
        public string? Name { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public string Display
        {
            get
            {
                return $"{Id}. {Name}";
            }
        }


        public Product() { 
        
            Name = string.Empty;
            Price = 0;
            Quantity = 0;
        
        }
         public override string ToString()
        {

            return Display ?? string.Empty;
        }
    } 
    

}
