using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public interface IPrinter
    {
        string Currency { get; set; }

        void PrintPriceCalculations(IProduct product);
    }
}