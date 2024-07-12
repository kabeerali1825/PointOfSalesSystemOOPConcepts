using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public class SaleItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Product.Price * Quantity;

        public SaleItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }

    public class SalesTransaction
    {
        public List<SaleItem> SaleItems { get; private set; } = new List<SaleItem>();

        public void AddProductToSale(Product product, int quantity)
        {
            var saleItem = new SaleItem(product, quantity);
            SaleItems.Add(saleItem);
        }

        public decimal CalculateTotalAmount()
        {
            return SaleItems.Sum(item => item.TotalPrice);
        }

        public string GenerateSalesTransactionsReceipt()
        {
            var receipt = new StringBuilder();
            receipt.AppendLine("Sales Transaction Receipt");
            receipt.AppendLine("-------------------------------");

            foreach (var item in SaleItems)
            {
                receipt.AppendLine($"{item.Product.Name} x {item.Quantity} = {item.TotalPrice:C}");
            }

            receipt.AppendLine("-------------------------------");
            receipt.AppendLine($"Total: {CalculateTotalAmount():C}");

            return receipt.ToString();
        }
    }

}
