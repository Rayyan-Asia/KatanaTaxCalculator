using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_Kata
{
    public class ProductPriceCalculator : IProductPriceCalculator
    {
        public double CalculatePriceWithGivenDiscountRateAfterTax(IProduct product, double priceAfterTax, decimal discountRate)
        {
            var totalDiscount = product.Discount + discountRate;
            if (totalDiscount > 1) { throw new Exception("Discount can't be greater than 100%"); }
            var priceWithDiscount = priceAfterTax - (priceAfterTax * (double)totalDiscount);
            return Math.Round(priceWithDiscount,2);
        }

        

        public double CalculatePriceWithGivenDiscountRate(IProduct product)
        {
            var totalDiscount = product.Discount;
            if (totalDiscount > 1) { throw new Exception("Discount can't be greater than 100%"); }
            var priceWithDiscount = product.Price - (product.Price * (double)totalDiscount);
            return Math.Round(priceWithDiscount, 2);
        }

        public double CalculatePriceWithGivenTaxRate(IProduct product, decimal taxRate)
        {
            var priceWithTax = (product.Price*(double)taxRate) + product.Price;
            return Math.Round(priceWithTax, 2);
        }

        public double CalculatePriceWithGivenTaxRateAfterDiscount(double priceAfterDiscount, decimal taxRate)
        {
            var priceWithTax = (priceAfterDiscount * (double)taxRate) + priceAfterDiscount;
            return Math.Round(priceWithTax, 2);
        }

        public double CalculatePriceWithGivenDiscountRateAfterTaxWithoutUPC(IProduct product, double priceAfterTax, decimal discountRate)
        {
            var totalDiscount = discountRate;
            if (totalDiscount > 1) { throw new Exception("Discount can't be greater than 100%"); }
            var priceWithDiscount = priceAfterTax - (priceAfterTax * (double)totalDiscount);
            return Math.Round(priceWithDiscount, 2);
        }

        public double CalculateExpense(IExpense expense, IProduct product) { 
            var totalExpenses = 0.0;
            if (expense.IsPercentage)
            {
                totalExpenses += Math.Round((product.Price * (double)(expense.Amount / 100.0)), 2);
            }
            else
            {
                totalExpenses += expense.Amount;
            }

            return totalExpenses;
        }

        public double CalculatePriceWithMultiplicativeDiscount(IProduct product , double priceAfterTax, decimal discountRate) 
        {
            var total = priceAfterTax;
            total -= Math.Round((total * (double)discountRate),2);
            if(product.Discount != 0)
            {
                total -= Math.Round((total * (double)product.Discount), 2);
            }
            return total;
        }
    }
}
