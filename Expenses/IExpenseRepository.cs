using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public interface IExpenseRepository
    {
        List<IExpense> GetAll();
        IExpense? GetExpenseByUpc(int upc);
    }
}