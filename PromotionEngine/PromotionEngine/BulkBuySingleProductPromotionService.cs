using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    class BulkBuySingleProductPromotionService : IPromotionService
    {
        public IPromotion Promotion { get; set; }

        public BulkBuySingleProductPromotionService(IPromotion promotion)
        {
            Promotion = promotion;
        }
        public decimal GetDiscountedPrice(Dictionary<string, int> products, Shop shop, bool applyDiscountOnMoreThanOneProduct)
        {
            decimal discountedCartPrice = 0;
            foreach (string sku in products.Keys)
            {
                bool promoCalculated = false;
                if (Promotion.ProductIdQtyMap.ContainsKey(sku))
                {
                    decimal individualProductDiscountedPrice = 0;
                    if (products.ContainsKey(sku) && products[sku] >= Promotion.ProductIdQtyMap[sku])
                    {
                        //Calculate Promotion Price
                        int comboQty = products[sku] / Promotion.ProductIdQtyMap[sku];
                        individualProductDiscountedPrice = comboQty * Promotion.Price + (products[sku] - comboQty * Promotion.ProductIdQtyMap[sku]) * shop.GetProductPrice(sku);
                        discountedCartPrice += individualProductDiscountedPrice;
                        promoCalculated = true;
                    }
                }

                //Add Regular Price
                if(!promoCalculated)
                    discountedCartPrice += products[sku] * shop.GetProductPrice(sku);
            }

            return discountedCartPrice;
            
        }
    }
}
