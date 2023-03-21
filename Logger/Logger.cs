using System.Net;

namespace Price_Calculator_Kata
{
    public class Logger : ILogger
    {
        public void PrintCapDeduction(double before, double after, string currency, int precision)
        {
            Console.WriteLine($"Discount Maxed Out at a {Math.Round(before - after, precision)} {currency} deduction");
        }

        public void PrintDiscount(double before, double after, decimal discountRate, string currency, int precision)
        {
            var deducedAmount = Math.Round((before - after), precision);
            var displayDiscountRate = Math.Round((discountRate) * 100);
            Console.WriteLine($"Price:{after} {currency} after {displayDiscountRate}% discount.\n-{deducedAmount} {currency} deduced.\n");
        }

        public void PrintExpense(string description, double amount, string currency, int precision)
        {
            Console.WriteLine($"Description: {description}\nCost: {Math.Round(amount,precision)} {currency}");
        }

        public void PrintFinalPrice(double amount, string currency, int precision)
        {
            Console.WriteLine($"Total: {Math.Round(amount,precision)} {currency}\n\n");
        }

        public void PrintMultiplicativeDiscount(double before, double after, decimal discount1, decimal discount2, string currency, int precision)
        {
            decimal displayDiscountRate1 = 0;
            double deducedPrice = 0;
            double deduced = 0;
            if (discount1 != 0)
            {
                displayDiscountRate1 = Math.Round((discount1) * 100);
                deducedPrice = Math.Round(before - (before * ((double)discount1)), precision);
                deduced = Math.Round((before * ((double)discount1)), precision );
            }
            var displayDiscountRate2 = Math.Round((discount2) * 100);
            Console.WriteLine($"Price Before: {before} {currency}");
            if (displayDiscountRate1 != 0)
            {
                Console.WriteLine($"Applied {displayDiscountRate1}% discount.");
                Console.WriteLine($"Amount deduced {deduced} {currency}");
            }
            Console.WriteLine($"Applied {displayDiscountRate2}% discount.");
            deduced = Math.Round((deducedPrice * ((double)discount2)), precision);
            Console.WriteLine($"Amount Deduced {deduced} {currency}");
            Console.WriteLine($"Price After Discounts: {after} {currency}");
        }

        public void PrintProduct(IProduct product, int precision)
        {
            Console.Write($"\n\n{product.Name} price reported as {Math.Round(product.Price,precision)} \n");
        }
        public void PrintTax(double before, double after, decimal taxRate, string currency, int precision)
        {
            var addedAmount = Math.Round((after - before), precision);
            var displayTaxRate = Math.Round(taxRate * 100);
            Console.WriteLine($"Price: {after} {currency} after {displayTaxRate}% tax  \n+{addedAmount} {currency} added\n");
        }
    }
}
