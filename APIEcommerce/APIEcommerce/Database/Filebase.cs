using ecommercelibrary.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace APIECommerce.Database
{
    public class Filebase
    {
        private string _root;
        private string _productRoot;
        private string _cartRoot;
        private static Filebase _instance;

        public static Filebase Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Filebase();
                }
                return _instance;
            }
        }

        private Filebase()
        {
            _root = @"C:\temp";
            _productRoot = Path.Combine(_root, "Products");
            _cartRoot = Path.Combine(_root, "Cart");

            if (!Directory.Exists(_productRoot))
                Directory.CreateDirectory(_productRoot);

            if (!Directory.Exists(_cartRoot))
                Directory.CreateDirectory(_cartRoot);
        }

        public List<Item?> Inventory
        {
            get
            {
                var result = new List<Item>();
                var root = new DirectoryInfo(_productRoot);
                foreach (var file in root.GetFiles())
                {
                    var item = JsonConvert.DeserializeObject<Item>(File.ReadAllText(file.FullName));
                    if (item != null)
                        result.Add(item);
                }
                return result;
            }
        }

        public int LastKey
        {
            get
            {
                if (Inventory.Any())
                {
                    return Inventory.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        public Item AddOrUpdate(Item item)
        {
            if (item.Id <= 0)
                item.Id = LastKey + 1;

            if (item.Product.Id == 0)
                item.Product.Id = item.Id;

            string path = Path.Combine(_productRoot, $"{item.Id}.json");

            if (File.Exists(path))
                File.Delete(path);

            File.WriteAllText(path, JsonConvert.SerializeObject(item));

            return item;
        }

        public bool Delete(string id)
        {
            string path = Path.Combine(_productRoot, $"{id}.json");

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }


        public List<Item> Cart
        {
            get
            {
                var result = new List<Item>();
                var root = new DirectoryInfo(_cartRoot);
                foreach (var file in root.GetFiles())
                {
                    var item = JsonConvert.DeserializeObject<Item>(File.ReadAllText(file.FullName));
                    if (item != null)
                        result.Add(item);
                }
                return result;
            }
        }

        public Item AddOrUpdateCartItem(Item item)
        {
            string path = Path.Combine(_cartRoot, $"{item.Id}.json");

            if (File.Exists(path))
                File.Delete(path);

            File.WriteAllText(path, JsonConvert.SerializeObject(item));
            return item;
        }

        public bool DeleteCartItem(int id)
        {
            string path = Path.Combine(_cartRoot, $"{id}.json");

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }

        public void ClearCart()
        {
            foreach (var file in Directory.GetFiles(_cartRoot))
            {
                File.Delete(file);
            }
        }
    }
}
