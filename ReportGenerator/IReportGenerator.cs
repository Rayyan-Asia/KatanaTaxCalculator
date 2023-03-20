using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public interface IReportGenerator
    {
        ILogger logger { get; set; }
        IDisplayMessages messenger { get; set; }
        IProductPriceCalculator priceCalculator { get; set; }
        IProductRepository repository { get; set; }

        void GenerateReport(decimal taxRate, decimal discountRate, bool discountfirst, int precision);
        void IterateAndReport(decimal taxRate, decimal discountRate, bool discountfirst, int precision, List<IProduct> products);
        void ReportCapReachedAtFirstDiscount(decimal taxRate, IProduct product, int precision);
        void ReportCapReachedAtSecondDiscount(decimal taxRate, decimal discountRate, IProduct product, int precision, double priceAfterDiscount, double cap, double priceAfterTax);
        void ReportDiscountAfter(decimal taxRate, decimal discountRate, IProduct product, double priceAfterTax, double priceAfterDiscount, bool multiplicative, int precision);
        void ReportDiscountFirst(decimal taxRate, decimal discountRate, IProduct product, int precision);
        void ReportProduct(decimal taxRate, decimal discountRate, IProduct product, int precision, double priceAfterDiscount, double priceAfterTax, double priceAfterSecondDiscount);
        void ReportWithoutCap(decimal taxRate, decimal discountRate, IProductPriceCalculator priceCalculator, ILogger logger, IProduct product, int precision, double priceAfterDiscount);
    }
}