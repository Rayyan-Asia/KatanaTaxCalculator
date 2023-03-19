using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_Kata
{
    public class Expense : IExpense
    {
        public double Amount { get; set; }
        public bool IsPercentage { get; set; }
        public string Description { get; set; }
    }
}
