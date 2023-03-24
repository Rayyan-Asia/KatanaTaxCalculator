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
