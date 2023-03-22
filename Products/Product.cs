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
        public decimal Price { get; set; }
    }
}
