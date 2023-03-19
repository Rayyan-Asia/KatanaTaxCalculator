namespace Price_Calculator_Kata
{
    public interface IProductPriceCalculator
    {
        public double CalculatePriceWithGivenTaxRate(IProduct product, decimal taxRate);
        public double CalculatePriceWithGivenDiscountRateAfterTax(IProduct product, double priceAfterTax, decimal discountRate);

        public double CalculatePriceWithGivenTaxRateAfterDiscount(double priceAfterDiscount, decimal taxRate);
        public double CalculatePriceWithGivenDiscountRate(IProduct product);
        public double CalculatePriceWithGivenDiscountRateAfterTaxWithoutUPC(IProduct product, double priceAfterTax, decimal discountRate);
        public double CalculateExpense(IExpense expense, IProduct product);
        public double CalculatePriceWithMultiplicativeDiscount(IProduct product, double priceAfterTax, decimal discountRate);
    }
}