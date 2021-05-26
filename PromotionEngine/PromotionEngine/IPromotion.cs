using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    interface IPromotion
    {
        string Name { get; set; }
        Dictionary<string, int> ProductIdQtyMap { get; set; }
        decimal Price { get; set; }
    }
}
