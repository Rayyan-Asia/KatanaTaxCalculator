using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public interface ICalculationResults
    {
        decimal BasePrice { get; set; }
        string Name { get; set; }
        decimal DiscountAmount { get; set; }
        List<IExpense>? CalculatedExpenses { get; set; }
        decimal FinalPrice { get; set; }
        decimal TaxAmount { get; set; }
    }
}