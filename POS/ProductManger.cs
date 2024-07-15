using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public class ProductManager
    {
        private List<Product> products = new List<Product> { 
            new Product(1, "Milk", 19.99m, 10, "Book", "Fiction"),
            new Product(2, "Balls", 29.99m, 5, "Cricket", "Non-Fiction"),
            new Product(3, "Antique Book", 99.99m, 2, "Book", "Rare"),
            new Product(4, "Book C", 15.99m, 20, "Book", "Children"),
            new Product(5, "Magazine A", 5.99m, 30, "Magazine", "Lifestyle")
          };
        

        // Add a new product
        public void AddProduct(int id ,string name, decimal price, int quantity, string type, string category)
        {
            var newProduct = new Product(id ,name, price, quantity, type, category);
            products.Add(newProduct);
        }

        // View all products
        public List<Product> ViewProducts()
        {
            return products;
        }

        // Update a product
        public bool UpdateProduct(int id ,string name=null, decimal? price = null, int? quantity = null, string type = null, string category = null)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(name)) product.Name = name;
                if (price.HasValue) product.Price = price.Value;
                if (quantity.HasValue) product.Quantity = quantity.Value;
                if (!string.IsNullOrEmpty(type)) product.Type = type;
                if (!string.IsNullOrEmpty(category)) product.Category = category;
                return true;
            }
            return false;
        }

        // Remove a product
        public bool RemoveProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
                return true;
            }
            return false;
        }

        public Product FindProductByID(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            return product;
        }
    }
}
