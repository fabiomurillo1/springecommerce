using APIECommerce.Database;
using ecommercelibrary.models;
using System.Linq;

namespace APIEcommerce.EC
{
    public class CartEC
    {
        public List<Item> Get()
        {
            return Filebase.Current.Cart;
        }

        public Item AddOrUpdate(Item item)
        {
            return Filebase.Current.AddOrUpdateCartItem(item);
        }

        public bool Remove(int id)
        {
            return Filebase.Current.DeleteCartItem(id);
        }

        public decimal GetTotal()
        {
            return Filebase.Current.Cart.Sum(i => (decimal?)(i.Quantity * (i.Product?.Price ?? 0))) ?? 0;

        }

        public void Checkout()
        {
            Filebase.Current.ClearCart();
        }

        public IEnumerable<Item> Search(string query)
        {
            return Filebase.Current.Cart
                .Where(i => i.Product?.Name?.ToLower().Contains(query.ToLower()) ?? false);
        }
    }
}
