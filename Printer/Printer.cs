using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatanaTaxCalculator;
using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public class Printer : IPrinter
    {
        private string Currency = "USD";

        public void PrintPriceCalculations(ICalculationResults Calculations)
        {

            Console.WriteLine($"Name: {Calculations.Name}\nBase Price: {Calculations.BasePrice.ConcatCurrency(Currency)}");
            Console.WriteLine($"Tax Amount= {Calculations.TaxAmount.ConcatCurrency(Currency)}\n");
            Console.WriteLine($"Discount Amount: {Calculations.DiscountAmount.ConcatCurrency(Currency)}\n");
            PrintExpenses(Calculations.CalculatedExpenses);
            Console.WriteLine($"Final Price = {Calculations.FinalPrice.ConcatCurrency(Currency)}\n\n");

        }

        private void PrintExpenses(List<IExpense>? expenses)
        {
            if (expenses != null)
            {
                foreach (IExpense expense in expenses)
                {
                    Console.WriteLine($"Description: {expense.Description}\n" +
                        $"Amount: {expense.Amount.ConcatCurrency(Currency)}\n");
                }
            }

        }
    }
}
