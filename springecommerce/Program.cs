using springecommerce.models;

namespace springecommerce
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            var lastKey = 0;
            Console.WriteLine("Welcome to Amazon!");
            Console.WriteLine("C. Create Item");
            Console.WriteLine("R. Read inventory items");
            Console.WriteLine("U. Update Inventory Item");
            Console.WriteLine("D. Delete Inventory Item");
            Console.WriteLine("Q. Quit");

            List<Product?> list = new List<Product?>();

            char choice;
            do
            {
                string? input = Console.ReadLine().ToUpper();
                choice = input[0];
                switch (choice)
                {

                    case 'C':
                        list.Add(new Product{ 

                            Id = lastKey++,
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
                        } 
                        break;
                    case 'D':
                        Console.WriteLine("Which product whould you like to delete?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        selectedProduct = list.FirstOrDefault(p => p.Id == selection);
                        list.Remove(selectedProduct);
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
