﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public  class PurchaseItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Product.Price * Quantity;

        public PurchaseItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
    public class PurchaseTransactions
    {
        private ProductManager productManager;
        public List<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();

        // Make the constructor public
        public PurchaseTransactions(ProductManager productManager)
        {
            this.productManager = productManager;
        }

        public void AddProductToPurchaseOrder(Product product, int quantity)
        {
            var purchaseItem = new PurchaseItem(product, quantity);
            PurchaseItems.Add(purchaseItem);
        }

        public decimal CalculateTotalPurchaseAmount()
        {
            return PurchaseItems.Sum(item => item.TotalPrice);
        }

        public string GeneratePurchaseReceipt()
        {
            var receipt = new StringBuilder();
            receipt.AppendLine("Purchase Receipt");
            receipt.AppendLine("-------------------------------");

            foreach (var item in PurchaseItems)
            {
                receipt.AppendLine($"{item.Product.Name} x {item.Quantity} = {item.TotalPrice:C}");
            }

            receipt.AppendLine("-------------------------------");
            receipt.AppendLine($"Total: {CalculateTotalPurchaseAmount():C}");

            return receipt.ToString();
        }
    }
}
