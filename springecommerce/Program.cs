using ecommercelibrary.services;
using springecommerce.models;

namespace springecommerce
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
           
            Console.WriteLine("Welcome to Amazon!");
            Console.WriteLine("1. Create Item");
            Console.WriteLine("2. Read inventory items");
            Console.WriteLine("3. Update Inventory Item");
            Console.WriteLine("4. Delete Inventory Item");
            Console.WriteLine("5. Add Shopping cart Items");
            Console.WriteLine("6. Read Shopping cart Items");
            Console.WriteLine("7. Delete Shopping cart Items");
            Console.WriteLine("8. Checkout");
            Console.WriteLine("9. Quit");

            List<Product?>list = productserviceproxy.Current.Products;
            List<Product?>shoppingcart = productserviceproxy.Current.Cart;

            char choice;
            do
            {
                string? input = Console.ReadLine();
                choice = input[0];
                switch (choice)
                {

                    case '1':
                        productserviceproxy.Current.AddOrUpdate(new Product
                        {
                            Name = Console.ReadLine(),
                            Quantity = int.Parse(Console.ReadLine()), 
                            Price = decimal.Parse(Console.ReadLine()) 
                        });
                        break;

                    case '2':
                        list.ForEach(Console.WriteLine);
                        break;
                    case '3':
                        Console.WriteLine("Which product whould you like to update?");
                        int selection = int.Parse(Console.ReadLine() ??  "-1");
                        var selectedProduct = list.FirstOrDefault(p => p.Id == selection); 
                        if(selectedProduct != null)
                        {
                            selectedProduct.Name = Console.ReadLine() ?? "Error";
                            selectedProduct.Quantity = int.Parse(Console.ReadLine());
                            selectedProduct.Price = decimal.Parse(Console.ReadLine());
                            productserviceproxy.Current.AddOrUpdate(selectedProduct);
                        } 
                        break;
                    case '4':
                        Console.WriteLine("Which product whould you like to delete from inventory?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        productserviceproxy.Current.Delete(selection);
                        break;
                    case '5':
                        Console.WriteLine("Which item would you like to add to your shopping cart?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        Console.WriteLine("How many would you like to add?");
                        int amount = int.Parse(Console.ReadLine() ?? "0");
                        var addedItem = productserviceproxy.Current.AddToCart(selection, amount);
                        if (addedItem == null)
                        {
                            Console.WriteLine("Failed to add item to cart (check stock or input).");
                        }
                        else
                        {
                            Console.WriteLine($"Added {amount} of '{addedItem.Name}' to the cart.");
                        }
                        break;
                    case '6':
                        Console.WriteLine("Your items in Shopping Cart");
                        shoppingcart.ForEach(Console.WriteLine);
                        break;
                    case '7':
                        Console.WriteLine("Which item would you like to delete from shopping cart?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        Console.WriteLine("How many would you like to delete ?");
                        amount = int.Parse(Console.ReadLine() ?? "0");
                        productserviceproxy.Current.DeleteFromCart(selection, amount);  
                        break;
                    case '8':
                        Console.WriteLine(productserviceproxy.Current.Checkout());
                        break;
                    case '9':
                        break;
                    default:
                        Console.WriteLine("Error : Unknown Command");
                        break;

                }
            } while (choice != 'Q');
        }

    }

}
