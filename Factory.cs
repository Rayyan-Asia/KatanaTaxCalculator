using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Price_Calculator_Kata;

namespace Price_Calculator_Kata
{
    public class Factory
    {
        public static IProductPriceCalculator CreateTaxCalculator() => new ProductPriceCalculator();

        public static IProductReportLogger CreateLogger() => new ProductReportLogger();

        public static IProduct CreateProduct() => new Product();

        public static IDisplayMessages CreateDisplayMessages() => new DisplayMessages();

        public static IProductRepository CreateProductRepository() => new ProductRepository();

        public static List<IExpense> CreateExpenses() => new List<IExpense>();

        public static IExpense CreateExpense() => new Expense();

    }
}
