using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public class InventoryManager
    {
        private ProductManager productManager;

        public InventoryManager(ProductManager productManager)
        {
            this.productManager = productManager;
        }

        public void TrackInventory()
        {
            var products = productManager.ViewProducts();
            Console.WriteLine("Current Inventory of Products:");
            productManager.DisplayInventoryTable(productManager);
            Console.ReadKey();
        }

        public bool ReceiveNewStock(int id, int quantity)
        {
            var product = productManager.ViewProducts()?.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                product.Quantity += quantity;
                productManager.UpdateProduct(product.Id,product.Name, product.Price, product.Quantity);
                return true;
            }

            return false;
        }

        public bool ReduceStock(int id, int quantity)
        {
            var product = productManager.ViewProducts().FirstOrDefault(p => p.Id == id);

            if (product != null && product.Quantity >= quantity)
            {
                product.Quantity -= quantity;
                productManager.UpdateProduct(product.Id,product.Name, product.Price, product.Quantity);
                return true;
            }

            return false;
        }

        public void ShowInventoryItems()
        {
            var productsInventory = productManager.ViewProducts();
            Console.WriteLine($"Products Count in Inventory: {productsInventory.Count()}");
            Console.WriteLine();

            var receipt = new StringBuilder();
            receipt.AppendLine("Products in Inventory Receipt");
            receipt.AppendLine("-------------------------------");

            foreach (var item in productsInventory)
            {
                receipt.AppendLine($"{item.Name} : {item.Quantity}");
            }

            Console.WriteLine(receipt.ToString());
            Console.ReadKey();
        }
    }
}
