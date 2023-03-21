using Katana_Tax_Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatanaTaxCalculator.KatanaTaxCalculator
{
    public class CalculatorConfigurations
    {
        public decimal DiscountRate;
        public decimal TaxRate;
        public DiscountCombinationType Type;

        public CalculatorConfigurations(decimal discountRate, decimal taxRate,
            DiscountCombinationType type)
        {
            DiscountRate = discountRate;
            TaxRate = taxRate;
            Type = type;
        }
    }
}
