using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    class Promotion : IPromotion
    {
        public string Name { get ; set ; }
        public Dictionary<string, int> ProductIdQtyMap { get ; set ; }
        public decimal Price { get ; set ; }
    }
}
