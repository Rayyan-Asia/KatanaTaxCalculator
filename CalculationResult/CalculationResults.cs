using Price_Calculator_Kata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Katana_Tax_Calculator
{
    public class CalculationResults : ICalculationResults
    {
        public decimal FinalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public List<IExpense>? CalculatedExpenses { get; set; }
        public decimal BasePrice { get; set; }
        public string Name { get; set; }

    }
}
