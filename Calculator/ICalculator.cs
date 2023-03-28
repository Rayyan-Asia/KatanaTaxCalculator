using Katana_Tax_Calculator;
using Price_Calculator_Kata;

namespace KatanaTaxCalculator
{
    public interface ICalculator
    {
        ICalculationResults CalculateAndSaveResults(int upc);
        decimal CalculateTaxAmount(IProduct product);
        decimal CalculateTotalDiscountAmount(IProduct product);
        decimal CalculateUniversalDiscount(decimal price);
    }
}