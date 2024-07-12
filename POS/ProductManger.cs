using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public class ProductManager
    {
        private List<Product> products = new List<Product>();

        // Add a new product
        public void AddProduct(string name, decimal price, int quantity, string type, string category)
        {
            var newProduct = new Product(name, price, quantity, type, category);
            products.Add(newProduct);
        }

        // View all products
        public List<Product> ViewProducts()
        {
            return products;
        }

        // Update a product
        public bool UpdateProduct(string name, decimal? price = null, int? quantity = null, string type = null, string category = null)
        {
            var product = products.FirstOrDefault(p => p.Name == name);
            if (product != null)
            {
                if (price.HasValue) product.Price = price.Value;
                if (quantity.HasValue) product.Quantity = quantity.Value;
                if (!string.IsNullOrEmpty(type)) product.Type = type;
                if (!string.IsNullOrEmpty(category)) product.Category = category;
                return true;
            }
            return false;
        }

        // Remove a product
        public bool RemoveProduct(string name)
        {
            var product = products.FirstOrDefault(p => p.Name == name);
            if (product != null)
            {
                products.Remove(product);
                return true;
            }
            return false;
        }
    }
}
