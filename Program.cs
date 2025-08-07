using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Inventory_Manager
{
    internal class Program
    {
        static Dictionary<string, int> inventory = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            PopulateInitialInventory();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. Add a new book");
                Console.WriteLine("2. Search for a book");
                Console.WriteLine("3. Update book quantity");
                Console.WriteLine("4. Display inventory");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        SearchBook();
                        break;
                    case "3":
                        UpdateBook();
                        break;
                    case "4":
                        DisplayInventory();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void PopulateInitialInventory()
        {
            Console.WriteLine("\nEnter initial books inventory (enter 'done' when finished):");
            while (true)
            {
                Console.Write("Enter book title (or 'done'): ");
                string title = Console.ReadLine().Trim();

                if (title.ToLower() == "done")
                    break;

                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Book title cannot be empty. Please try again.");
                    continue;
                }

                Console.Write($"Enter quantity for '{title}': ");
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity >= 0)
                {
                    inventory[title] = quantity;
                }
                else
                {
                    Console.WriteLine("Invalid quantity. Please enter a non-negative integer.");
                }
            }
        }
        static void AddBook()
        {
            Console.Write("\nEnter book title to add: ");
            string title = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Book title cannot be empty.");
                return;
            }

            if (inventory.ContainsKey(title))
            {
                Console.WriteLine($"'{title}' already exists in inventory with quantity {inventory[title]}.");
                return;
            }

            Console.Write($"Enter quantity for '{title}': ");
            if (int.TryParse(Console.ReadLine(), out int quantity) && quantity >= 0)
            {
                inventory[title] = quantity;
                Console.WriteLine($"Added '{title}' with quantity {quantity}.");
            }
            else
            {
                Console.WriteLine("Invalid quantity. Please enter a non-negative integer.");
            }
        }
        static void SearchBook()
        {
            Console.Write("\nEnter book title to search: ");
            string title = Console.ReadLine().Trim();

            if (inventory.TryGetValue(title, out int quantity))
            {
                Console.WriteLine($"Found '{title}' - Quantity: {quantity}");
            }
            else
            {
                Console.WriteLine($"'{title}' not found in inventory.");
            }
        }
        static void UpdateBook()
        {
            Console.Write("\nEnter book title to update: ");
            string title = Console.ReadLine().Trim();

            if (!inventory.ContainsKey(title))
            {
                Console.WriteLine($"'{title}' not found in inventory.");
                return;
            }

            Console.WriteLine($"Current quantity for '{title}': {inventory[title]}");
            Console.Write("Enter new quantity: ");
            if (int.TryParse(Console.ReadLine(), out int newQuantity) && newQuantity >= 0)
            {
                inventory[title] = newQuantity;
                Console.WriteLine($"Updated '{title}' quantity to {newQuantity}.");
            }
            else
            {
                Console.WriteLine("Invalid quantity. Please enter a non-negative integer.");
            }
        }
        static void DisplayInventory()
        {
            Console.WriteLine("\nCurrent Inventory:");
            Console.WriteLine("------------------");
            Console.WriteLine("{0,-30} {1}", "Title", "Quantity");
            Console.WriteLine("--------------------------------");

            foreach (var book in inventory.OrderBy(b => b.Key))
            {
                Console.WriteLine("{0,-30} {1}", book.Key, book.Value);
            }
        }
    }
}
