using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public interface IConsoleInputHandler
    {
        IProductRepository repository { get; set; }
        IDisplayMessages messenger { get; set; }
        bool DemandsCustomDiscount();
        bool DemandsDiscountBeforeTax();
        void ExitFromInvalidInput(string input);
        void GetCustomDiscountCap(int precision);
        void GetCustomDiscounts();
        void GetCustomExpenses( int precision);
        void GetDiscountCap(IProduct product, int precision);
        void GetExpenseValueAndDescription(IProduct product, int precision);
        bool IsYes();
        decimal ParseDecimal();
        double ParseDouble( string input, int precision);
        int ParseInt( string input);
        int ParsePercentage(string input);
    }
}