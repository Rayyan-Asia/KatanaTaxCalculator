using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Katana_Tax_Calculator;

namespace Price_Calculator_Kata
{
    public class Expense : IExpense
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int UPC { get; set; }
        public RelativeCalculationType CalculationType { get; set; }

        public Expense(decimal amount, string description, int uPC, RelativeCalculationType calculationType)
        {
            Amount = amount;
            Description = description;
            UPC = uPC;
            CalculationType = calculationType;
        }

        public override string ToString()
        {
            return Description + ": " +Amount;
        }
    }
}
