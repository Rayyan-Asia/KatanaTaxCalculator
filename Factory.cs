using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Katana_Tax_Calculator;
using KatanaTaxCalculator.KatanaTaxCalculator;
using Price_Calculator_Kata;

namespace Price_Calculator_Kata
{
    public static class Factory
    {
        public static ILogger CreateLogger() => new Logger();
        public static IProduct CreateProduct() => new Product();
        public static IConsoleMessenger CreateDisplayMessages() => new ConsoleMessenger();
        public static IProductRepository CreateProductRepository() => new ProductRepository();
        public static IDiscountRepository CreateDiscountRepository() => new DiscountRepository();
        public static ICapRepository CreateCapRepository() => new CapRepository();
        public static IExpenseRepository CreateExpenseRepository() => new ExpenseRepository();
        public static IConsoleInputHandler CreateConsoleInputHandler() => new InputHandler(CreateDisplayMessages());
    }
}
