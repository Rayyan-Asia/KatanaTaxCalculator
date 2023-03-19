namespace Price_Calculator_Kata
{
    public interface IProductReportLogger
    {
        void PrintProduct(IProduct product);
        
        void PrintDiscount(double before, double after, decimal discountRate);
        void PrintMultiplicativeDiscount(double before, double after, decimal discount1,decimal discount2);
        void PrintTax(double before, double after, decimal taxRate);
        void PrintExpense(string description, double amount);

        void PrintFinalPrice(double amount);
    }
}