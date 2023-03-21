using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Katana_Tax_Calculator
{
    public static class ExtensionMethods
    {
        public static decimal Round(this decimal value, int precision)
        {
            return Math.Round(value, precision);
        }

        public static string ConcatCurrency(this decimal value, string currency)
        {
            return value + " " + currency;
        }
    }
}
