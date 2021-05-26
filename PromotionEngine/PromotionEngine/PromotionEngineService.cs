using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    class PromotionEngineService
    {
        private IPromotionService? promotionService = null ;

        public void SetPromotionService(IPromotionService promotionService)
        {
            this.promotionService = promotionService;
        }
        public decimal GetDiscountedPrice(Dictionary<string, int> cartProducts, Shop shop, bool applyDiscountOnMoreThanOneProduct)
        {
            decimal discountedCartPrice = 0;
            if(promotionService == null)
            {
                throw new NullReferenceException("Promotion Service is null, kindly set the same before the call.");
            }
            else
            {
                discountedCartPrice = promotionService.GetDiscountedPrice(cartProducts, shop, applyDiscountOnMoreThanOneProduct);
            }
            return discountedCartPrice;
        }
    }
}
