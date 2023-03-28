using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;
        public ExpenseService(IExpenseRepository repository)
        {
            _repository = repository;
        }

        public decimal CalculateExpensesAndExport(int upc, decimal price, List<IExpense> calculatedExpenses)
        {
            var expenses = GetExpensesByUpc(upc);
            decimal sum = 0.0m;
            foreach (IExpense expense in expenses)
            {
                if (expense.CalculationType == RelativeCalculationType.Percent)
                {
                    var amount = (expense.Amount / 100.0m) * price;
                    sum += amount;
                    calculatedExpenses.Add(Factory.CreateExpense(amount, expense.Description, upc, RelativeCalculationType.Amount));
                }
                else
                {
                    sum += expense.Amount;
                    calculatedExpenses.Add(expense);
                }
            }

            return sum;
        }

        public List<IExpense> GetAll()
        {
            return _repository.GetAll();
        }

        public List<IExpense> GetExpensesByUpc(int upc)
        {
            return _repository.GetExpensesByUpc(upc);
        }
    }
}
