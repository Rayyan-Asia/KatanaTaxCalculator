using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Price_Calculator_Kata;

namespace Price_Calculator_Kata
{
    public class Product : IProduct
    {
        public string Name { get; set; }
        public int UPC { get; set; }
        public double Price { get; set; }
        public decimal Discount { get; set; } = 0;
        public double DiscountCap { get; set; }
        public List<IExpense> Expenses { get; set; } = Factory.CreateExpenses();
        
    }
}
