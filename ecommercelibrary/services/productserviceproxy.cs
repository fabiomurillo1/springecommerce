using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using springecommerce.models;

namespace ecommercelibrary.services
{
    public class productserviceproxy
    {
        private productserviceproxy() 
        {
            Products = new List<Product?>();
            Cart = new List<Product>(); 
        }
        private int LastKey
        {
            get
            {
                if(!Products.Any())
                {
                    return 0;
                }
                return Products.Select(p => p?.Id ?? 0).Max();
            }
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
        
        public List<Product?> Products { get; private set; }
        public List<Product?> Cart { get; private set; }


        public Product AddOrUpdate(Product product) 
        {
            if(product.Id == 0)
            {
                product.Id = LastKey + 1;
                Products.Add(product);
            }
            

            return product;
        }
        
        public Product? Delete(int id)
        {
            if(id == 0)
            {
                return null; 
            }

            Product? product = Products.FirstOrDefault(p => p.Id == id); 
            Products.Remove(product);
            return product;
        }

        public Product? DeleteFromCart(int id, int amount)
        {
            if (id == 0 || amount <= 0) return null;
            Product? cartItem = Cart.FirstOrDefault(p => p.Id == id);
            if (cartItem == null || cartItem.Quantity < amount) return null;
            Product? inventoryItem = Products.FirstOrDefault(p => p.Id == id);
            if (inventoryItem != null)
            {
                inventoryItem.Quantity += amount;
            }
            cartItem.Quantity -= amount;
            if (cartItem.Quantity == 0)
            {
                Cart.Remove(cartItem);
            }

            return cartItem;
        }


        public Product? AddToCart(int id, int amount)
        {
            var product = Products.FirstOrDefault(p => p?.Id == id);

            if (product == null || amount <= 0 || product.Quantity < amount)
            {
                return null;
            }

            product.Quantity -= amount;
            var cartItem = Cart.FirstOrDefault(p => p?.Id == id);
            if (cartItem != null)
            {
                cartItem.Quantity += amount;
                return cartItem;
            }
            var newCartItem = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = amount
            };
            Cart.Add(newCartItem);
            return newCartItem;
        }

        public string Checkout()
        {
            if (!Cart.Any())
            {
                return "Your shopping cart is empty. Nothing to checkout.";
            }

            StringBuilder receipt = new StringBuilder();
            receipt.AppendLine("===== Receipt =====");
            decimal subtotal = 0;

            foreach (var item in Cart)
            {
                if (item != null)
                {
                    decimal lineTotal = (item.Price ?? 0) * (item.Quantity ?? 0);
                    subtotal += lineTotal;
                    receipt.AppendLine($"{item.Name} x {item.Quantity} @ ${item.Price:F2} = ${lineTotal:F2}");
                }
            }

            decimal tax = subtotal * 0.07m;
            decimal total = subtotal + tax;

            receipt.AppendLine("--------------------");
            receipt.AppendLine($"Subtotal: ${subtotal:F2}");
            receipt.AppendLine($"Sales Tax (7%): ${tax:F2}");
            receipt.AppendLine($"Total: ${total:F2}");
            receipt.AppendLine("====================");

            Cart.Clear(); 

            return receipt.ToString();
        }

    }


}
