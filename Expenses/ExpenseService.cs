using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public class ExpenseService
    {
        private readonly IExpenseRepository _repository;
        public ExpenseService(IExpenseRepository repository)
        {
            _repository = repository;
        }

        public List<IExpense> GetAll()
        {
            return _repository.GetAll();
        }

        public IExpense? GetExpenseByUpc(int upc)
        {
            return _repository.GetExpenseByUpc(upc);
        }
    }
}
