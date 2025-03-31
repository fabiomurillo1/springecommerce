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

            List<string?> list = new List<string?>();

            char choice;
            do
            {
                string? input = Console.ReadLine().ToUpper();
                choice = input[0];
                switch (choice)
                {

                    case 'C':
                        AddProduct(list);
                        break;
                    case 'R':

                    case 'U':
                    case 'D':
                    case 'Q':
                        break;
                    default:
                        Console.WriteLine("Error : Unknown Command");
                        break;

                }
            } while (choice != 'Q');
        }



        static void AddProduct(List<string> list)
        {
            var newProduct = Console.ReadLine() ?? "N/A";
            list.Add(newProduct);
        }
    }

}
