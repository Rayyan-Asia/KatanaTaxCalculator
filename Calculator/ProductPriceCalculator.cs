using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_Kata
{
    public class ProductPriceCalculator : IProductPriceCalculator
    {
        public double CalculatePriceWithGivenDiscountRateAfterTax(IProduct product, double priceAfterTax, decimal discountRate, int precision)
        {
            var totalDiscount = product.Discount + discountRate;
            if (totalDiscount > 1) { throw new Exception("Discount can't be greater than 100%"); }
            var priceWithDiscount = priceAfterTax - (priceAfterTax * (double)totalDiscount);
            return Math.Round(priceWithDiscount,precision);
        }

        

        public double CalculatePriceWithGivenDiscountRate(IProduct product, int precision)
        {
            var totalDiscount = product.Discount;
            if (totalDiscount > 1) { throw new Exception("Discount can't be greater than 100%"); }
            var priceWithDiscount = product.Price - (product.Price * (double)totalDiscount);
            return Math.Round(priceWithDiscount, precision);
        }

        public double CalculatePriceWithGivenTaxRate(IProduct product, decimal taxRate, int precision)
        {
            var priceWithTax = (product.Price*(double)taxRate) + product.Price;
            return Math.Round(priceWithTax, precision);
        }

        public double CalculatePriceWithGivenTaxRateAfterDiscount(double priceAfterDiscount, decimal taxRate, int precision)
        {
            var priceWithTax = (priceAfterDiscount * (double)taxRate) + priceAfterDiscount;
            return Math.Round(priceWithTax, precision);
        }

        public double CalculatePriceWithGivenDiscountRateAfterTaxWithoutUPC(IProduct product, double priceAfterTax, decimal discountRate, int precision)
        {
            var totalDiscount = discountRate;
            if (totalDiscount > 1) { throw new Exception("Discount can't be greater than 100%"); }
            var priceWithDiscount = priceAfterTax - (priceAfterTax * (double)totalDiscount);
            return Math.Round(priceWithDiscount, precision);
        }

        public double CalculateExpense(IExpense expense, IProduct product, int precision) { 
            var totalExpenses = 0.0;
            if (expense.IsPercentage)
            {
                totalExpenses += Math.Round((product.Price * (double)(expense.Amount / 100.0)), precision);
            }
            else
            {
                totalExpenses += expense.Amount;
            }

            return totalExpenses;
        }

        public double CalculatePriceWithMultiplicativeDiscount(IProduct product , double priceAfterTax, decimal discountRate, int precision) 
        {
            var total = priceAfterTax;
            total -= Math.Round((total * (double)discountRate),precision);
            if(product.Discount != 0)
            {
                total -= Math.Round((total * (double)product.Discount), precision);
            }
            return total;
        }
    }
}
