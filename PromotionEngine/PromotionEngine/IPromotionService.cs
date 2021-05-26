using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    interface IPromotionService
    {
        public IPromotion Promotion { get; set; }
        public decimal GetDiscountedPrice(Dictionary<string, int> products, Shop shop, bool applyDiscountOnMoreThanOneProduct);
    }
}
