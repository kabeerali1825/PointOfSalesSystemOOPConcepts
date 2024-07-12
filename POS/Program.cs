using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS;

class Program
{
    static void Main(string[] args)
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("****************WELCOME TO POS CONSOLE APPLICATION**************");
        Console.WriteLine("***********************************************************************************");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine();
        Console.WriteLine();

        UserManager userManager = new UserManager();
        ProductManager productManager = new ProductManager();
        InventoryManager inventoryManager = new InventoryManager(productManager);
        SalesTransaction salesTransaction = new SalesTransaction();
        PurchaseTransaction purchaseOrder = new PurchaseTransaction(productManager);
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("1. Register User");
            Console.WriteLine("2. Log In");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    RegisterUser(userManager);
                    break;
                case "2":
                    LogInUser(userManager, productManager, inventoryManager, salesTransaction, purchaseOrder);
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void RegisterUser(UserManager userManager)
    {
        Console.Write("Enter Your name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Your email: ");
        string email = Console.ReadLine();
        Console.Write("Enter Your password: ");
        string password = Console.ReadLine();

        Console.WriteLine("Select role:");
        Console.WriteLine("1. Admin");
        Console.WriteLine("2. Cashier");
        string roleOption = Console.ReadLine();
        UserRoles role = null;

        switch (roleOption)
        {
            case "1":
                role = Roles.Admin;
                break;
            case "2":
                role = Roles.Cashier;
                break;
            default:
                Console.WriteLine("Invalid role selected.");
                return;
        }

        userManager.RegisterUser(name, email, password, role);
        Console.WriteLine("User registered successfully.");
    }

    static void LogInUser(UserManager userManager, ProductManager productManager, InventoryManager inventoryManager, SalesTransaction salesTransaction, PurchaseOrder purchaseOrder)
    {
        Console.Write("Enter email: ");
        string email = Console.ReadLine();
        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        var user = userManager.LogInUserAuthentication(email, password);

        if (user != null)
        {
            Console.WriteLine($"Welcome, {user.Name}");

            if (userManager.IsAdmin(user))
            {
                AdminMenu(productManager, inventoryManager);
            }
            else if (userManager.IsCashier(user))
            {
                CashierMenu(productManager, salesTransaction, purchaseOrder);
            }
        }
        else
        {
            Console.WriteLine("Invalid email or password.");
        }
    }

    static void AdminMenu(ProductManager productManager, InventoryManager inventoryManager)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n--- Admin Menu ---");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Product");
            Console.WriteLine("3. Remove Product");
            Console.WriteLine("4. Add Product to Inventory");
            Console.WriteLine("5. Update Product in Inventory");
            Console.WriteLine("6. Remove Product from Inventory");
            Console.WriteLine("7. View Products");
            Console.WriteLine("8. Back to Main Menu");
            Console.Write("Select an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    productManager.AddProduct();
                    break;
                case "2":
                    productManager.UpdateProduct();
                    break;
                case "3":
                    productManager.RemoveProduct();
                    break;
                case "4":
                    inventoryManager.AddProductToInventory();
                    break;
                case "5":
                    inventoryManager.UpdateProductInInventory();
                    break;
                case "6":
                    inventoryManager.RemoveProductFromInventory();
                    break;
                case "7":
                    productManager.ViewProducts();
                    break;
                case "8":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void CashierMenu(ProductManager productManager, SalesTransaction salesTransaction, PurchaseTransaction purchaseOrder)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n--- Cashier Menu ---");
            Console.WriteLine("1. Add Product to Sale");
            Console.WriteLine("2. Calculate Total Amount");
            Console.WriteLine("3. Generate Receipt for Sale");
            Console.WriteLine("4. Add Product to Purchase Order");
            Console.WriteLine("5. Calculate Total Amount for Purchase");
            Console.WriteLine("6. Generate Receipt for Purchase");
            Console.WriteLine("7. View Products");
            Console.WriteLine("8. Back to Main Menu");
            Console.Write("Select an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    salesTransaction.AddProductToSale(product,int);
                    break;
                case "2":
                    Console.WriteLine($"Total Amount: {salesTransaction.CalculateTotalAmount():C}");
                    break;
                case "3":
                    Console.WriteLine(salesTransaction.GenerateSalesTransactionsReceipt());
                    break;
                case "4":
                    purchaseOrder.AddProduct(product,int);
                    break;
                case "5":
                    Console.WriteLine($"Total Amount for Purchase: {purchaseOrder.CalculateTotalAmount():C}");
                    break;
                case "6":
                    Console.WriteLine(purchaseOrder.GeneratePurchaseReceipt());
                    break;
                case "7":
                    productManager.ViewProducts();
                    break;
                case "8":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
