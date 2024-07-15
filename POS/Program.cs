using System;
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
                            AdminMenu(productManager, inventoryManager,userManager);
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
            string? name = Console.ReadLine();
            Console.Write("Enter Your email: ");
            string? email = Console.ReadLine();
            Console.Write("Enter Your password: ");
            string? password = Console.ReadLine();

            Console.WriteLine("Select role:");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. Cashier");
            string? roleOption = Console.ReadLine();
            UserRoles role = new UserRoles();

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
            string? email = Console.ReadLine();
            Console.Write("Enter password: ");
            string? password = Console.ReadLine();

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

    static void AdminMenu(ProductManager productManager, InventoryManager inventoryManager, UserManager userManager)
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            SetConsoleColors();
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("******************************** ADMIN MENU ***************************************");
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("1. Add New Product to the Inventory");
            Console.WriteLine("2. Update Product Information in Inventory");
            Console.WriteLine("3. Remove Product From Inventory");
            Console.WriteLine("4. View Inventory Products");
            Console.WriteLine("5. Receive New Stock");
            Console.WriteLine("6. Reduce Stock ");
            Console.WriteLine("7. Change User Role");
            Console.WriteLine("8. Log Out");
            Console.Write("Select an option: ");
            string? option = Console.ReadLine();

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
                    inventoryManager.TrackInventory();
                    break;
                case "5":
                    ReceiveNewStock(inventoryManager,productManager);
                    break;
                case "6":
                    ReduceTheStock(inventoryManager, productManager);
                    break;
                case "7":
                    ChangeUserRole(userManager);
                    break;
                case "8":
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
            Console.WriteLine("2. Calculate Sales Total Amount");
            Console.WriteLine("3. Generate Receipt for Completed Sales Transactions");
            Console.WriteLine("4. Add Product to Purchase Order");
            Console.WriteLine("5. Calculate Purchase Total Amount");
            Console.WriteLine("6. Generate Purchase Receipt");
            Console.WriteLine("7. Log Out");
            Console.Write("Select an option: ");
            string? option = Console.ReadLine();

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
                    AddProductToPurchase(productManager, purchaseTransactions);
                    break;
                case "5":
                    Console.WriteLine($"Total Amount: {purchaseTransactions.CalculateTotalPurchaseAmount():C}");
                    break;
                case "6":
                    Console.WriteLine(purchaseTransactions.GeneratePurchaseReceipt());
                    break;
                case "7":
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

    static void ChangeUserRole(UserManager userManager)
    {
        Console.Clear();
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("******************************* CHANGE USER ROLE **********************************");
        Console.WriteLine("***********************************************************************************");

        Console.Write("Enter user's email: ");
        string? email = Console.ReadLine();

        Console.WriteLine("Select new role:");
        Console.WriteLine("1. Admin");
        Console.WriteLine("2. Cashier");
        string? roleOption = Console.ReadLine();
        UserRoles newRole;

        switch (roleOption)
        {
            case "1":
                newRole = Roles.Admin;
                break;
            case "2":
                newRole = Roles.Cashier;
                break;
            default:
                PrintErrorMessage("Invalid role selected.");
                return;
        }

        if (userManager.ChangeUserRole(email, newRole))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("User role updated successfully.");
        }
        else
        {
            PrintErrorMessage("User not found.");
        }

        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    
    static void AddProduct(ProductManager productManager)
    {
        Console.Clear();
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("********************************* ADD PRODUCT INVENTORY****************************");
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine();
        Console.WriteLine("Available Products to Add In Inventory:");
        productManager.DisplayInventoryTable(productManager);
        Console.WriteLine();
        Console.Write("Enter product ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Enter product name: ");
        string? name = Console.ReadLine();
        Console.Write("Enter product price: ");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.Write("Enter product quantity: ");
        int quantity = int.Parse(Console.ReadLine());
        Console.Write("Enter product type: ");
        string? type = Console.ReadLine();
        Console.Write("Enter product category: ");
        string? category = Console.ReadLine();

        productManager.AddProduct(id,name, price, quantity, type, category);
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
        Console.WriteLine();
        Console.WriteLine("Available Products:");
        productManager.DisplayInventoryTable(productManager);
        Console.Write("Enter product ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Enter product name to update: ");
        string? name = Console.ReadLine();
        Console.Write("Enter new price (or leave blank to keep current): ");
        string? priceInput = Console.ReadLine();
        decimal? price = string.IsNullOrEmpty(priceInput) ? (decimal?)null : decimal.Parse(priceInput);
        Console.Write("Enter new quantity (or leave blank to keep current): ");
        string? quantityInput = Console.ReadLine();
        int? quantity = string.IsNullOrEmpty(quantityInput) ? (int?)null : int.Parse(quantityInput);
        Console.Write("Enter new type (or leave blank to keep current): ");
        string? type = Console.ReadLine();
        Console.Write("Enter new category (or leave blank to keep current): ");
        string? category = Console.ReadLine();

        if (productManager.UpdateProduct(id,name, price, quantity, type, category))
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
        Console.WriteLine();
        Console.WriteLine("Available Products:");
        productManager.DisplayInventoryTable(productManager);
        Console.Write("Enter product ID to Remove: ");
        int id = int.Parse(Console.ReadLine());
        if (productManager.RemoveProduct(id))
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

    static void ReceiveNewStock(InventoryManager inventoryManager, ProductManager productManager)
    {
        Console.Clear();
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("****************************** RECEIVE NEW STOCK **********************************");
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("Available Products:");
        productManager.DisplayInventoryTable(productManager);
        Console.Write("Enter product ID to ADD new Stock: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Enter quantity to add: ");
        int quantity = int.Parse(Console.ReadLine());

        bool flag= inventoryManager.ReceiveNewStock(id, quantity);
        Console.WriteLine(flag);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Stock updated successfully.");
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    static void ReduceTheStock(InventoryManager inventoryManager, ProductManager productManager)
    {
        Console.Clear();
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("****************************** REDUCE THE STOCK **********************************");
        Console.WriteLine("***********************************************************************************");
        Console.WriteLine("Available Products:");
        productManager.DisplayInventoryTable(productManager);
        Console.Write("Enter product ID to Reduce The Stock: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Enter quantity to Reduce from Stock: ");
        int quantity = int.Parse(Console.ReadLine());

        inventoryManager.ReduceStock(id, quantity);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Stock updated successfully.");
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void AddProductToSale(ProductManager productManager, SalesTransaction salesTransaction)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("******************************* ADD PRODUCT TO SALE *******************************");
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("Available Products:");
            productManager.DisplayInventoryTable(productManager);

            Console.Write("Enter product ID to add to sale: ");
            int id = int.Parse(Console.ReadLine());

            var product = productManager.ViewProducts().FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                Console.Write("Enter quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                if (quantity <= product.Quantity)
                {
                    salesTransaction.AddProductToSale(new Product(id, product.Name, product.Price, quantity, product.Type, product.Category));
                    product.Quantity -= quantity;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Product added to sale.");
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
        }
        catch (FormatException)
        {
            PrintErrorMessage("Invalid input. Please enter a valid number.");
        }
        catch (Exception ex)
        {
            PrintErrorMessage($"An error occurred: {ex.Message}");
        }
        finally
        {
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    static void AddProductToPurchase(ProductManager productManager, PurchaseTransactions purchaseTransactions)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("***************************** ADD PRODUCT TO PURCHASE *****************************");
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("Available Products:");
            productManager.DisplayInventoryTable(productManager);

            Console.Write("Enter product ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            var product = productManager.FindProductByID(id);

            if (product != null && quantity <= product.Quantity)
            {
                purchaseTransactions.AddProductToPurchaseOrder(new Product(id, product.Name, product.Price, quantity,
                    product.Type, product.Category), quantity);
                product.Quantity -= quantity;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Product added to purchase order successfully.");
            }
            else
            {
                PrintErrorMessage("Product not found or insufficient quantity.");
            }
        }
        catch (FormatException)
        {
            PrintErrorMessage("Invalid input. Please enter a valid number.");
        }
        catch (Exception ex)
        {
            PrintErrorMessage($"An error occurred: {ex.Message}");
        }
        finally
        {
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

}
