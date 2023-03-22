using Katana_Tax_Calculator;
using Price_Calculator_Kata;

namespace KatanaTaxCalculator
{
    public interface ICalculator
    {
        int _precision { get; set; }
        decimal DiscountRate { get; set; }
        decimal TaxRate { get; set; }
        DiscountCombinationType type { get; set; }

        decimal CalculateCap(IProduct product, Cap cap);
        decimal CalculateExpense(IProduct product, IExpense expense);
        decimal CalculateMultiplicativeDiscount(IProduct product);
        decimal CalculateProductDiscount(IProduct product);
        decimal CalculateTaxAmount(IProduct product);
        decimal CalculateTotalDiscountAmount(IProduct product, Cap? cap);
        decimal CalculateTotalPrice(IProduct product, decimal ExpenseSum, Cap? cap);
        decimal CalculateUniversalDiscount(decimal price);
        decimal CalculteSimpleSumDiscount(IProduct product);
        bool IsDiscountBefore(IProduct product);
    }
}