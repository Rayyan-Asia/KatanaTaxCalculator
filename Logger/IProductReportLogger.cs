namespace Price_Calculator_Kata
{
    public interface IProductReportLogger
    {
        void PrintProduct(IProduct product, int precision);
        
        void PrintDiscount(double before, double after, decimal discountRate, string currency, int precision);
        void PrintMultiplicativeDiscount(double before, double after, decimal discount1,decimal discount2, string currency, int precision);
        void PrintTax(double before, double after, decimal taxRate, string currency, int precision);
        void PrintExpense(string description, double amount, string currency, int precision);

        void PrintFinalPrice(double amount, string currency, int precision);

        void PrintCapDeduction(double before, double after, string currency, int precision);
    }
}