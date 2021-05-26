using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    class Shop
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public void AddProductToShop(Product product)
        {
            var sameSKU = from products in Products
            where products.skuID == product.skuID
            select products;

            if (sameSKU.Count() == 0)
                Products.Add(product);
            else
                sameSKU.First().Price = product.Price;
        }

        public void AddProductToShop(string productDetails)
        {
            try
            {
                var productInfo = productDetails.Split(',');
                if (productInfo.Length > 1)
                {
                    Product product = new Product()
                    {
                        skuID = productInfo[0],
                        Price = Convert.ToDecimal(productInfo[1])
                    };
                    AddProductToShop(product);
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public decimal GetProductPrice(string sku)
        {
            var sameSKU = from products in Products
                          where products.skuID == sku
                          select products;
            if (sameSKU.Count() != 0)
                return sameSKU.First().Price;

            return 0m;
        }

        public Product GetProductDetails(int productLocation)
        {
            if(Products.Count >= productLocation)
            {
                return Products[productLocation];
            }

            return null;
        }
    }
}
