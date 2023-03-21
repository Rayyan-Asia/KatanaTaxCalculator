using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public interface IConsoleInputHandler
    {
        bool DemandsDiscountBeforeTax();
        void ExitFromInvalidInput(string input);
        bool UserEnteredYes();
        decimal ParseDecimal();
        double ParseDouble( string input, int precision);
        int ParseInt( string input);
        int ParsePercentage(string input);
    }
}