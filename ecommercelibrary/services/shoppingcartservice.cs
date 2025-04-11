using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecommercelibrary.models;
using springecommerce.models;

namespace ecommercelibrary.services
{
    public class shoppingcartservice
    {
        private productserviceproxy _prodSvc;
        private List<Item> items;
        private List<Item> CartItems
        {
            get
            {
                return items;   
            }

        }
        public static shoppingcartservice Current 
        {
            get
            {
                if (instance == null)
                {
                    instance = new shoppingcartservice();   
                }
                return instance;
            }
                
        }

        private static shoppingcartservice? instance;
        private shoppingcartservice() 
        {
            items = new List<Item>();
        }
    }
}
