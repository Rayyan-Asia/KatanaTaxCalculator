using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly List<IExpense> _expenses;

        public ExpenseRepository()
        {
            _expenses = new List<IExpense>()
            {
                new Expense(10,"Import",123456,RelativeCalculationType.Percent),
                new Expense(10,"Shipping",654321,RelativeCalculationType.Amount),
            };
        }

        public List<IExpense> GetAll() { return _expenses; }
        public List<IExpense> GetExpensesByUpc(int upc)
        {
            return (from expense in _expenses where expense.UPC == upc select expense).ToList();
        }
    }
}
