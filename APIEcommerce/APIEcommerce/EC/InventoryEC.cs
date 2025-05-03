using APIEcommerce.Database;
using APIECommerce.Database;
using ecommercelibrary.DTO;
using ecommercelibrary.models;
using springecommerce.models;

namespace APIEcommerce.EC
{
    public class InventoryEC
    {
       
        public List<Item?> Get()
        {
            return Filebase.Current.Inventory;
         
        }

        public IEnumerable<Item> Get(string? query)
        {
            return FakeDatabase.Search(query).Take(100) ??  new List<Item>();
        }

        public Item? Delete(int id)
        {
            var itemToDelete = FakeDatabase.Inventory.FirstOrDefault(i => i.Id == id);

            if (itemToDelete != null)
            {
                FakeDatabase.Inventory.Remove(itemToDelete);

                Filebase.Current.Delete(id.ToString());
            }

            return itemToDelete;
        }



        public Item? AddOrUpdate(Item item)
        {
            item.Price = item.Product?.Price ?? 0;
            return Filebase.Current.AddOrUpdate(item);
               
        }
    }
}
