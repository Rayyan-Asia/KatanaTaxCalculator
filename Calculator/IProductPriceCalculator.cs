namespace Price_Calculator_Kata
{
    public interface IProductPriceCalculator
    {
        public double CalculatePriceWithGivenTaxRate(IProduct product, decimal taxRate, int precision);
        public double CalculatePriceWithGivenDiscountRateAfterTax(IProduct product, double priceAfterTax, decimal discountRate, int precision);

        public double CalculatePriceWithGivenTaxRateAfterDiscount(double priceAfterDiscount, decimal taxRate, int precision);
        public double CalculatePriceWithGivenDiscountRate(IProduct product, int precision);
        public double CalculatePriceWithGivenDiscountRateAfterTaxWithoutUPC(IProduct product, double priceAfterTax, decimal discountRate, int precision);
        public double CalculateExpense(IExpense expense, IProduct product, int precision);
        public double CalculatePriceWithMultiplicativeDiscount(IProduct product, double priceAfterTax, decimal discountRate, int precision);
        public double CalculateExpenses(IProductPriceCalculator priceCalculator, ILogger logger, IProduct product, int precision);
    }
}