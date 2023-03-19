namespace Price_Calculator_Kata
{
    public interface IProductReportLogger
    {
        void PrintProduct(IProduct product);
        
        void PrintDiscount(double before, double after, decimal discountRate, string currency);
        void PrintMultiplicativeDiscount(double before, double after, decimal discount1,decimal discount2, string currency);
        void PrintTax(double before, double after, decimal taxRate, string currency);
        void PrintExpense(string description, double amount, string currency);

        void PrintFinalPrice(double amount, string currency);

        void PrintCapDeduction(double before, double after, string currency);
    }
}