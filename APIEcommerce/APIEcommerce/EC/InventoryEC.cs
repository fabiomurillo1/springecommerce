using APIEcommerce.Database;
using ecommercelibrary.DTO;
using ecommercelibrary.models;

namespace APIEcommerce.EC
{
    public class InventoryEC
    {
       
        public List<Item?> Get()
        {
            return FakeDatabase.Inventory;
         
        }
    }
}
