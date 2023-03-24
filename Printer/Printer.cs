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
        private readonly ICalculator _calculator;
        private readonly IExpenseService _expenseService;
        private readonly ICapService _capService;
        public string Currency { get; set; }

        public Printer(ICalculator calculator, IExpenseService expenseService, ICapService capService, string currency)
        {
            _calculator = calculator;
            _expenseService = expenseService;
            _capService = capService;
            Currency = currency;
        }

        public void PrintPriceCalculations(IProduct product)
        {
            int precision = _calculator.Precision;
            Console.WriteLine($"Name: {product.Name}\nBase Price: {product.Price.Round(precision).ConcatCurrency(Currency)}\n" +
                $"UPC: {product.UPC}");
            Console.WriteLine($"Tax Percent= {_calculator.TaxRate * 100}");
            Console.WriteLine($"Tax Amount= {_calculator.CalculateTaxAmount(product).ConcatCurrency(Currency)}\n");
            ICap? discountCap = _capService.GetCapByUpc(product.UPC);
            Console.WriteLine($"Discount Amount: {_calculator.CalculateTotalDiscountAmount(product, discountCap).ConcatCurrency(Currency)}\n");
            
            var totalExpense = CalculateAndPrintExpenses(product);
            Console.WriteLine($"Final Price = {_calculator.CalculateTotalPrice(product, totalExpense, discountCap)}\n\n");

        }

        private decimal CalculateAndPrintExpenses(IProduct product)
        {
            decimal totalExpense = 0;
            List<IExpense> list = _expenseService.GetExpensesByUpc(product.UPC);
            foreach (IExpense expense in list)
            {
                decimal expenseValue = 0;
                if (expense.CalculationType == RelativeCalculationType.Amount)
                {
                    expenseValue = expense.Amount.Round(_calculator.Precision);
                }
                else
                {
                    expenseValue = _calculator.CalculateExpense(product, expense);
                }
                totalExpense += expenseValue;
                Console.WriteLine($"Description: {expense.Description}\n" +
                    $"Amount: {expenseValue.ConcatCurrency(Currency)}\n");
            }

            return totalExpense;
        }
    }
}
