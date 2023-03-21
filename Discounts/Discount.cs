using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katana_Tax_Calculator
{
    public class Discount : IDiscount
    {
        public decimal Percentage { get; set; }
        public int UPC { get; set; }
        public DiscountOrderType type { get; set; }

        public Discount(decimal percentage, int uPC, DiscountOrderType type)
        {
            Percentage = percentage;
            UPC = uPC;
            this.type = type;
        }
    }
}
