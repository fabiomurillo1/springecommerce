using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ecommercelibrary.DTO;
using ecommercelibrary.models;
using ecommercelibrary.Utilities;
using ecommercelibrary.Utility;
using Newtonsoft.Json;
using springecommerce.models;

namespace ecommercelibrary.services
{
    public class productserviceproxy
    {
        private productserviceproxy() 
        {
            var productPayload = new WebRequestHandler().Get("/Inventory").Result;
            Products = JsonConvert.DeserializeObject<List<Item>>(productPayload) ?? new List<Item?>();
        }
        
        private static productserviceproxy? instance;
        private static object instanceLock = new object();
        public static productserviceproxy? Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new productserviceproxy();
                    }
                    return instance;
                }
            }
        }
        
        public List<Item?> Products { get; private set; }
        public List<Product?> Cart { get; private set; }

        public async Task<IEnumerable<Item?>> Search(string? query)
        {
            if (query == null)
            {
                return new List<Item>();
            }
            var response = await new WebRequestHandler().Post("/Inventory/Search", new QueryRequest {Query = query});
            Products = JsonConvert.DeserializeObject<List<Item?>>(response) ?? new List<Item?>();
            return Products;
        }
        public Item AddOrUpdate(Item item) 
        {
            var response = new WebRequestHandler().Post("/Inventory", item).Result;
            var newItem = JsonConvert.DeserializeObject<Item>(response);
            if (newItem == null)
            {
                return item;
            }
            if(item.Id == 0) 
            {
                Products.Add(newItem);
            }
            else
            {
                var existingitem = Products.FirstOrDefault(p => p.Id == item.Id);
                var index  = Products.IndexOf(existingitem);
                Products.RemoveAt(index);
                Products.Insert(index, new Item(newItem));
                
            }
            return item;
        }

        public async Task Refresh()
        {
            var productPayload = await new WebRequestHandler().Get("/Inventory");
            Products = JsonConvert.DeserializeObject<List<Item>>(productPayload) ?? new List<Item?>();
        }

        public Item? PurchaseItem(Item? item) 
        {
            if (item.Id <= 0 || item == null) 
            {
                return null;
            }

            var itemtopurchase = GetById(item.Id);
            if (itemtopurchase != null)
            {
                itemtopurchase.Quantity--;
            }

            return itemtopurchase;
        }
        
        public Item? Delete(int id)
        {
            if(id == 0)
            {
                return null; 
            }

            var result = new WebRequestHandler().Delete($"/Inventory/Delete/{id}").Result;

            Item? product = Products.FirstOrDefault(p => p.Id == id); 
            Products.Remove(product);
            return JsonConvert.DeserializeObject<Item>(result);
        }

        public Item? GetById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }


        }

    }



