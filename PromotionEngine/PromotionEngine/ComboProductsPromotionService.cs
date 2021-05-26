using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    class ComboProductsPromotionService : IPromotionService
    {
        public IPromotion Promotion { get ; set ; }

        public ComboProductsPromotionService(IPromotion promotion)
        {
            Promotion = promotion;
        }

        public decimal GetDiscountedPrice(Dictionary<string, int> products, Shop shop, bool applyDiscountOnMoreThanOneProduct)
        {
            decimal discountedCartPrice = 0;
            if (Promotion.ProductIdQtyMap.Keys.All(k => products.ContainsKey(k)))
            {
                decimal individualProductDiscountedPrice = 0;
                foreach (string sku in products.Keys)
                {
                    if (Promotion.ProductIdQtyMap.ContainsKey(sku))
                        individualProductDiscountedPrice = (products[sku] - Promotion.ProductIdQtyMap[sku]) * shop.GetProductPrice(sku);
                    else
                        individualProductDiscountedPrice = products[sku] * shop.GetProductPrice(sku);

                    discountedCartPrice += individualProductDiscountedPrice;
                }
                discountedCartPrice += Promotion.Price;
            }
            return discountedCartPrice;
        }
    }
}
