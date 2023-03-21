using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katana_Tax_Calculator
{
    public enum DiscountOrderType
    {
        AfterTax,
        BeforeTax,
    }

    public enum RelativeCalculationType
    {
        Percent,
        Amount
    }

    public enum DiscountCombinationType
    {
        Additive,
        Multiplicative
    }
}
