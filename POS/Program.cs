﻿using System;
using POS;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        SetConsoleColors();
        PrintWelcomeMessage();

        UserManager userManager = new UserManager();
        ProductManager productManager = new ProductManager();
        InventoryManager inventoryManager = new InventoryManager(productManager);
        PurchaseTransactions purchaseTransactions = new PurchaseTransactions(productManager);
        SalesTransaction salesTransaction = new SalesTransaction();

        bool exit = false;

        while (!exit)
        {
            PrintMainMenu();
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    RegisterUser(userManager);
                    break;
                case "2":
                    var user = LogInUser(userManager);
                    if (user != null)
                    {
                        if (userManager.IsAdmin(user))
                        {
                            AdminMenu(productManager, inventoryManager);
                        }
                        else if (userManager.IsCashier(user))
                        {
                            CashierMenu(productManager, purchaseTransactions, salesTransaction);
                        }
                    }
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    PrintErrorMessage("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void SetConsoleColors()
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();
    }

    static void PrintWelcomeMessage()
    {
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("****************WELCOME TO POS CONSOLE APPLICATION*********************************");
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine();
        Console.ResetColor();
    }

    static void PrintMainMenu()
    {
        Console.Clear();
        SetConsoleColors();
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("********************************** MAIN MENU **************************************");
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("1. Register User");
        Console.WriteLine("2. Log In");
        Console.WriteLine("3. Exit");
        Console.Write("Select an option: ");
    }

    static void PrintErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void RegisterUser(UserManager userManager)
    {
        Console.Clear();
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("******************************** REGISTER USER ************************************");
        Console.WriteLine("***********************************************************************************");

        try
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("User registered successfully.");
            Console.ResetColor();
        }
        catch (ArgumentException ex)
        {
            PrintErrorMessage($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static Users LogInUser(UserManager userManager)
    {
        Console.Clear();
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("********************************** LOG IN *****************************************");
        Console.WriteLine("***********************************************************************************");

        try
        {
            Console.Write("Enter email: ");
            string email = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            var user = userManager.LogInUserAuthentication(email, password);

            if (user != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Welcome, {user.Name}");
                Console.ResetColor();
                return user;
            }
            else
            {
                PrintErrorMessage("Invalid email or password.");
                return null;
            }
        }
        catch (ArgumentException ex)
        {
            PrintErrorMessage($"Error: {ex.Message}");
            return null;
        }
    }

    static void AdminMenu(ProductManager productManager, InventoryManager inventoryManager)
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            SetConsoleColors();
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("******************************** ADMIN MENU ***************************************");
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Product");
            Console.WriteLine("3. Remove Product");
            Console.WriteLine("4. View Inventory");
            Console.WriteLine("5. Receive New Stock");
            Console.WriteLine("6. Reduce Stock ");
            Console.WriteLine("7. Log Out");
            Console.Write("Select an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddProduct(productManager);
                    break;
                case "2":
                    UpdateProduct(productManager);
                    break;
                case "3":
                    RemoveProduct(productManager);
                    break;
                case "4":
                    inventoryManager.ShowInventoryItems();
                    break;
                case "5":
                    ReceiveNewStock(inventoryManager);
                    break;
                case "6":
                    ReduceTheStock(inventoryManager);
                    break;
                case "7":
                    exit = true;
                    break;
                default:
                    PrintErrorMessage("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void CashierMenu(ProductManager productManager, PurchaseTransactions purchaseTransactions, SalesTransaction salesTransaction)
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            SetConsoleColors();
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("******************************* CASHIER MENU **************************************");
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("1. Add Product to Sale");
            Console.WriteLine("2. Calculate Total Amount");
            Console.WriteLine("3. Generate Receipt");
            Console.WriteLine("4. Log Out");
            Console.Write("Select an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddProductToSale(productManager, salesTransaction);
                    break;
                case "2":
                    Console.WriteLine($"Total Amount: {salesTransaction.CalculateTotalAmount():C}");
                    break;
                case "3":
                    Console.WriteLine(salesTransaction.GenerateSalesTransactionsReceipt());
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    PrintErrorMessage("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    static void AddProduct(ProductManager productManager)
    {
        Console.Clear();
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("********************************** ADD PRODUCT ************************************");
        Console.WriteLine("***********************************************************************************");

        Console.Write("Enter product name: ");
        string name = Console.ReadLine();
        Console.Write("Enter product price: ");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.Write("Enter product quantity: ");
        int quantity = int.Parse(Console.ReadLine());
        Console.Write("Enter product type: ");
        string type = Console.ReadLine();
        Console.Write("Enter product category: ");
        string category = Console.ReadLine();

        productManager.AddProduct(name, price, quantity, type, category);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Product added successfully.");
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void UpdateProduct(ProductManager productManager)
    {
        Console.Clear();
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("******************************* UPDATE PRODUCT ************************************");
        Console.WriteLine("***********************************************************************************");

        Console.Write("Enter product name to update: ");
        string name = Console.ReadLine();
        Console.Write("Enter new price (or leave blank to keep current): ");
        string priceInput = Console.ReadLine();
        decimal? price = string.IsNullOrEmpty(priceInput) ? (decimal?)null : decimal.Parse(priceInput);
        Console.Write("Enter new quantity (or leave blank to keep current): ");
        string quantityInput = Console.ReadLine();
        int? quantity = string.IsNullOrEmpty(quantityInput) ? (int?)null : int.Parse(quantityInput);
        Console.Write("Enter new type (or leave blank to keep current): ");
        string type = Console.ReadLine();
        Console.Write("Enter new category (or leave blank to keep current): ");
        string category = Console.ReadLine();

        if (productManager.UpdateProduct(name, price, quantity, type, category))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product updated successfully.");
            Console.ResetColor();
        }
        else
        {
            PrintErrorMessage("Product not found.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void RemoveProduct(ProductManager productManager)
    {
        Console.Clear();
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("******************************* REMOVE PRODUCT ************************************");
        Console.WriteLine("***********************************************************************************");

        Console.Write("Enter product name to remove: ");
        string name = Console.ReadLine();
        if (productManager.RemoveProduct(name))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product removed successfully.");
            Console.ResetColor();
        }
        else
        {
            PrintErrorMessage("Product not found.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void ReceiveNewStock(InventoryManager inventoryManager)
    {
        Console.Clear();
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("****************************** RECEIVE NEW STOCK **********************************");
        Console.WriteLine("***********************************************************************************");

        Console.Write("Enter product name: ");
        string productName = Console.ReadLine();
        Console.Write("Enter quantity to add: ");
        int quantity = int.Parse(Console.ReadLine());

        inventoryManager.ReceiveNewStock(productName, quantity);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Stock updated successfully.");
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    static void ReduceTheStock(InventoryManager inventoryManager)
    {
        Console.Clear();
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("****************************** REDUCE THE STOCK **********************************");
        Console.WriteLine("***********************************************************************************");

        Console.Write("Enter product name: ");
        string productName = Console.ReadLine();
        Console.Write("Enter quantity to Reduce from Stock: ");
        int quantity = int.Parse(Console.ReadLine());

        inventoryManager.ReduceStock(productName, quantity);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Stock updated successfully.");
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void AddProductToSale(ProductManager productManager, SalesTransaction salesTransaction)
    {
        Console.Clear();
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("******************************* ADD PRODUCT TO SALE *******************************");
        Console.WriteLine("***********************************************************************************");

        Console.Write("Enter product name to add to sale: ");
        string name = Console.ReadLine();
        var product = productManager.ViewProducts().FirstOrDefault(p => p.Name == name);
        if (product != null)
        {
            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            if (quantity <= product.Quantity)
            {
                salesTransaction.AddProductToSale(new Product(name, product.Price, quantity, product.Type, product.Category));
                product.Quantity -= quantity;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Product added to sale.");
                Console.ResetColor();
            }
            else
            {
                PrintErrorMessage("Insufficient quantity in stock.");
            }
        }
        else
        {
            PrintErrorMessage("Product not found.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
