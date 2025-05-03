using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ecommercelibrary.models;
using Newtonsoft.Json;
using springecommerce.models;

namespace ecommercelibrary.services
{
    public class shoppingcartservice
    {
        private productserviceproxy _prodSvc = productserviceproxy.Current;
        private List<Item> items;
        private static shoppingcartservice? instance;

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

        private shoppingcartservice()
        {
            items = new List<Item>();
        }

        public List<Item> CartItems => items;

        private readonly string baseUrl = "https://localhost:7267/Cart";

        public async Task<Item?> AddOrUpdate(Item item)
        {
            var existinginvitem = _prodSvc.GetById(item.Id);
            if (existinginvitem == null || existinginvitem.Quantity == 0)
            {
                return null;
            }
            existinginvitem.Quantity--;
            _prodSvc.AddOrUpdate(existinginvitem);

            var existingitem = CartItems.FirstOrDefault(p => p.Id == item.Id);
            if (existingitem == null)
            {
                var newItem = new Item(item) { Quantity = 1 };
                CartItems.Add(newItem);
                await PostToApiAsync(newItem);
            }
            else
            {
                existingitem.Quantity++;
                await PostToApiAsync(existingitem);
            }

            return existinginvitem;
        }

        public async Task<Item?> ReturnItem(Item? item)
        {
            if (item == null || item.Id <= 0)
            {
                return null;
            }

            var itemToReturn = CartItems.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null)
            {
                itemToReturn.Quantity--;

                if (itemToReturn.Quantity <= 0)
                {
                    CartItems.Remove(itemToReturn);
                    await DeleteFromApiAsync(item.Id);
                }
                else
                {
                    await PostToApiAsync(itemToReturn);
                }

                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id);
                if (inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(itemToReturn));
                }
                else
                {
                    inventoryItem.Quantity++;
                    _prodSvc.AddOrUpdate(inventoryItem);
                }
            }

            return itemToReturn;
        }

        public async Task CheckoutAsync()
        {
            var client = new HttpClient();
            await client.PostAsync(baseUrl + "/Checkout", null);
            CartItems.Clear();
        }

        private async Task PostToApiAsync(Item item)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(baseUrl, content);
        }

        private async Task DeleteFromApiAsync(int id)
        {
            var client = new HttpClient();
            await client.DeleteAsync(baseUrl + $"/{id}");
        }
    }
}