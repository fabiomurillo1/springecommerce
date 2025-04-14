using ecommercelibrary.DTO;
using ecommercelibrary.models;

namespace APIEcommerce.Database
{
    public static class FakeDatabase
    {
        public static List<Item?> Inventory = new List<Item?>
        {
                new Item {Product = new ProductDTO{Id = 1, Name ="Product 1"}, Id = 1, Quantity = 1},
                new Item {Product = new ProductDTO{Id = 2, Name ="Product 2"}, Id = 2, Quantity = 2 },
                new Item {Product = new ProductDTO{Id = 3, Name ="Product 3"},  Id = 3, Quantity = 3 }

            };
    }
}
