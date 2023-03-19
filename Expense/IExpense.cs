namespace Price_Calculator_Kata
{
    public interface IExpense
    {
        double Amount { get; set; }
        string Description { get; set; }
        bool IsPercentage { get; set; }
    }
}