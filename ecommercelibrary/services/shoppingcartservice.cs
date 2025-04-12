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
        private productserviceproxy _prodSvc = productserviceproxy.Current;
        private List<Item> items;
        public List<Item> CartItems
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

        public Item? AddOrUpdate(Item item) 
        {
            var existinginvitem = _prodSvc.GetById(item.Id);
            if (existinginvitem == null || existinginvitem.Quantity == 0)
            {
                return null;
            }
            if (existinginvitem != null)
            {
                existinginvitem.Quantity--;
            }

            var existingitem = CartItems.FirstOrDefault(p => p.Id == item.Id);
            if (existingitem == null) 
            {
                var newItem = new Item(item);
                newItem.Quantity = 1;
                CartItems.Add(newItem);


            }
            else 
            {
                existingitem.Quantity++;
            }

            return existinginvitem;
        }

        public Item? ReturnItem(Item? item)
        {
            if (item.Id <= 0 || item == null)
            {
                return null;
            }

            var itemtoreturn = CartItems.FirstOrDefault(c => c.Id == item.Id);
            if (itemtoreturn != null)
            {
                itemtoreturn.Quantity--;
                var inventoryitem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemtoreturn.Id);
                if(inventoryitem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(itemtoreturn));
                }
                else
                {
                    inventoryitem.Quantity++;
                }
            }

            
            return itemtoreturn;
        }
    }
}
