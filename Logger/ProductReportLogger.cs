﻿namespace Price_Calculator_Kata
{
    public class ProductReportLogger : IProductReportLogger
    {
        public void PrintCapDeduction(double before, double after, string currency)
        {
            Console.WriteLine($"Discount Maxed Out at a {Math.Round(before - after, 2)} {currency} deduction");
        }

        public void PrintDiscount(double before, double after, decimal discountRate, string currency)
        {
            var deducedAmount = Math.Round((before - after), 2);
            var displayDiscountRate = Math.Round((discountRate) * 100);
            Console.WriteLine($"Price:{after} {currency} after {displayDiscountRate}% discount.\n-{deducedAmount} {currency} deduced.\n");
        }

        public void PrintExpense(string description, double amount, string currency)
        {
            Console.WriteLine($"Description: {description}\nCost: {amount} {currency}");
        }

        public void PrintFinalPrice(double amount, string currency)
        {
            Console.WriteLine($"Total: {amount} {currency}\n\n");
        }

        public void PrintMultiplicativeDiscount(double before, double after, decimal discount1, decimal discount2, string currency)
        {
            decimal displayDiscountRate1 = 0;
            double deducedPrice = 0;
            double deduced = 0;
            if (discount1 != 0)
            {
                displayDiscountRate1 = Math.Round((discount1) * 100);
                deducedPrice = Math.Round(before - (before * ((double)discount1)), 2);
                deduced = Math.Round((before * ((double)discount1)), 2);
            }
            var displayDiscountRate2 = Math.Round((discount2) * 100);
            Console.WriteLine($"Price Before: {before} {currency}");
            if (displayDiscountRate1 != 0)
            {
                Console.WriteLine($"Applied {displayDiscountRate1}% discount.");
                Console.WriteLine($"Amount deduced {deduced} {currency}");
            }
            Console.WriteLine($"Applied {displayDiscountRate2}% discount.");
            deduced = Math.Round((deducedPrice * ((double)discount2)), 2);
            Console.WriteLine($"Amount Deduced {deduced} {currency}");
            Console.WriteLine($"Price After Discounts: {after} {currency}");
        }

        public void PrintProduct(IProduct product)
        {
            Console.Write($"{product.Name} price reported as {product.Price} {product.Currency}\n");
        }
        public void PrintTax(double before, double after, decimal taxRate, string currency)
        {
            var addedAmount = Math.Round((after - before), 2);
            var displayTaxRate = Math.Round(taxRate * 100);
            Console.WriteLine($"Price: {after} {currency} after {displayTaxRate}% tax  \n+{addedAmount} {currency} added\n");
        }
    }
}
