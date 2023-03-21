using Katana_Tax_Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatanaTaxCalculator.KatanaTaxCalculator
{
    public class Calculator
    {
        public decimal TaxRate { get; set; }
        public decimal DiscountRate { get; set; }
        private readonly DiscountService _discountService;
        public DiscountCombinationType type { get; set; }

        public Calculator(decimal taxRate, decimal discountRate, DiscountService discountService, DiscountCombinationType type)
        {
            TaxRate = taxRate;
            DiscountRate = discountRate;
            this._discountService = discountService;
            this.type = type;
        }
    }
}
