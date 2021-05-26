using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    class Cart
    {
        public Dictionary<string, int> Products { get; set; } = new Dictionary<string, int>();
        public decimal TotalPrice { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public bool ApplyDiscountOnMoreThanOneProduct { get; private set; }
        public bool ApplyMoreThanOnePromotion { get; private set; }

        public Cart(bool applyDiscountOnMoreThanOneProduct, bool applyMoreThanOnePromotion)
        {
            ApplyMoreThanOnePromotion = applyMoreThanOnePromotion;
            ApplyDiscountOnMoreThanOneProduct = applyDiscountOnMoreThanOneProduct;
        }

        public void AddProductToCart(Product product, int qty)
        {
            if (product != null && product.skuID != string.Empty)
            {
                if (Products.ContainsKey(product.skuID))
                {
                    Products[product.skuID] += qty;
                }
                else
                    Products.Add(product.skuID, qty);

                TotalPrice += qty * product.Price;
            }
        }
    }
}
