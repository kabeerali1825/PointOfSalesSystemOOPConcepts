﻿using System;
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
            Console.WriteLine("Current Inventory:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} - Quantity: {product.Quantity}");
            }
            Console.ReadKey();
        }
        public void ReceiveNewStock(string productName, int quantity)
        {
            var InventoryItems = productManager.ViewProducts().FirstOrDefault(p => p.Name == productName);
            if (InventoryItems != null)
            {
                InventoryItems.Quantity += quantity;
            }
        }
        public bool ReduceStock(string productName, int quantity)
        {
            var product = productManager.ViewProducts().FirstOrDefault(p => p.Name == productName);
            if (product != null && product.Quantity >= quantity)
            {
                product.Quantity -= quantity;
                return true;
            }
            return false;
        }
        public void ShowInventoryItems()
        {
            var productsInventory = productManager.ViewProducts();
            Console.WriteLine($"Products Count in Inventory: {productsInventory.Count()}");

            var receipt = new StringBuilder();
            receipt.AppendLine("Products in Inventory Receipt");
            receipt.AppendLine("-------------------------------");

            foreach (var item in productsInventory)
            {
                Console.WriteLine(receipt.AppendLine($"{item.Name} :  {item.Quantity} "));
            }
            Console.ReadKey();

        }


    }
}
