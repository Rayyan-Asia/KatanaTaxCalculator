using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Katana_Tax_Calculator;
using Price_Calculator_Kata;

namespace Price_Calculator_Kata
{
    public class Factory
    {
        

        public static ILogger CreateLogger() => new Logger();

        public static IProduct CreateProduct() => new Product();

        public static IDisplayMessages CreateDisplayMessages() => new DisplayMessages();

        public static IProductRepository CreateProductRepository() => new ProductRepository();

        public static List<IExpense> CreateExpenses() => new List<IExpense>();
        public static IConsoleInputHandler CreateConsoleInputHandler() => new InputHandler(CreateProductRepository(),CreateDisplayMessages());
    }
}
