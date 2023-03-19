using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_Kata
{
    public class ProductReportLogger : IProductReportLogger
    {
        public void PrintDiscount(double before, double after, decimal discountRate)
        {
            var deducedAmount = Math.Round((before - after), 2);
            var displayDiscountRate = Math.Round((discountRate) * 100);
            Console.WriteLine($"Price:{after} after {displayDiscountRate}% discount.\n-{deducedAmount} deduced.\n");
        }

        public void PrintExpense(string description, double amount)
        {
            Console.WriteLine($"Description: {description}\nCost: {amount}");
        }

        public void PrintFinalPrice(double amount)
        {
            Console.WriteLine($"Total: {amount}\n\n");
        }

        public void PrintMultiplicativeDiscount(double before, double after, decimal discount1, decimal discount2)
        {
            decimal displayDiscountRate1=0;
            double deducedPrice = 0;
            double deduced = 0;
            if (discount1 != 0)
            {
                displayDiscountRate1 = Math.Round((discount1) * 100);
                deducedPrice = Math.Round(before - (before * ((double)discount1)),2);
                deduced = Math.Round((before * ((double)discount1)), 2);
            }
            var displayDiscountRate2 = Math.Round((discount2) * 100);
            Console.WriteLine($"Price Before: {before}");
            if (displayDiscountRate1 != 0) {
                Console.WriteLine($"Applied {displayDiscountRate1}% discount.");
                Console.WriteLine($"Amount deduced {deduced}");
            }
            Console.WriteLine($"Applied {displayDiscountRate2}% discount.");
            deduced = Math.Round((deducedPrice * ((double)discount2)), 2);
            Console.WriteLine($"Amount Deduced {deduced}");
            Console.WriteLine($"Price After Discounts: {after}");
        }

        public void PrintProduct(IProduct product)
        {
            Console.Write($"{product.Name} price reported as {product.Price}\n");
        }
        public void PrintTax( double before, double after, decimal taxRate)
        {
            var addedAmount = Math.Round((after - before), 2);
            var displayTaxRate = Math.Round(taxRate * 100);
            Console.WriteLine($"Price: {after} after {displayTaxRate}% tax  \n+{addedAmount} added\n");
        }
    }
}
