using Katana_Tax_Calculator;

namespace Price_Calculator_Kata
{
    public interface IExpense
    {
        decimal Amount { get; set; }
        string? Description { get; set; }
        RelativeCalculationType CalculationType { get; set; }
        public int UPC { get; set; }
    }
}