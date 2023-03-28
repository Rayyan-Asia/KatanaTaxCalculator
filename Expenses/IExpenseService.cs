using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public interface IExpenseService
    {
        List<IExpense> GetAll();
        List<IExpense> GetExpensesByUpc(int upc);
        decimal CalculateExpensesAndExport(int upc, decimal price, List<IExpense> expenses); 
    }
}