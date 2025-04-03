using ecommercelibrary.services;
using springecommerce.models;

namespace springecommerce
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
           
            Console.WriteLine("Welcome to Amazon!");
            Console.WriteLine("C. Create Item");
            Console.WriteLine("R. Read inventory items");
            Console.WriteLine("U. Update Inventory Item");
            Console.WriteLine("D. Delete Inventory Item");
            Console.WriteLine("Q. Quit");

            List<Product?> list = productserviceproxy.Current.Products;

            char choice;
            do
            {
                string? input = Console.ReadLine().ToUpper();
                choice = input[0];
                switch (choice)
                {

                    case 'C':
                        productserviceproxy.Current.AddOrUpdate(new Product
                        {
                            Name = Console.ReadLine()
                        });
                        break;
                    case 'R':
                        list.ForEach(Console.WriteLine);
                        break;
                    case 'U':
                        Console.WriteLine("Which product whould you like to update?");
                        int selection = int.Parse(Console.ReadLine() ??  "-1");
                        var selectedProduct = list.FirstOrDefault(p => p.Id == selection); 
                        if(selectedProduct != null)
                        {
                            selectedProduct.Name = Console.ReadLine() ?? "Error";
                            productserviceproxy.Current.AddOrUpdate(selectedProduct);
                        } 
                        break;
                    case 'D':
                        Console.WriteLine("Which product whould you like to delete?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        productserviceproxy.Current.Delete(selection);
                        break;
                    case 'Q':
                        break;
                    default:
                        Console.WriteLine("Error : Unknown Command");
                        break;

                }
            } while (choice != 'Q');
        }

    }

}
