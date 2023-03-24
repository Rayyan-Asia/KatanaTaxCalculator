﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Katana_Tax_Calculator;
using Katana_Tax_Calculator.Product;
using KatanaTaxCalculator;
using Price_Calculator_Kata;

namespace Price_Calculator_Kata
{
    public static class Factory
    {
        public static IProduct CreateProduct() => new Product();
        public static IConsoleMessenger CreateDisplayMessages() => new ConsoleMessenger();
        private static IProductRepository CreateProductRepository() => new ProductRepository();
        private static IDiscountRepository CreateDiscountRepository() => new DiscountRepository();
        private static ICapRepository CreateCapRepository() => new CapRepository();
        private static IExpenseRepository CreateExpenseRepository() => new ExpenseRepository();
        public static IProductService CreateProductService() => new ProductService(CreateProductRepository());
        public static IDiscountService CreateDiscountService() => new DiscountService(CreateDiscountRepository());
        public static ICapService CreateCapService() => new CapService(CreateCapRepository());
        public static IExpenseService CreateExpenseService() => new ExpenseService(CreateExpenseRepository());
        public static IConsoleReader CreateConsoleInputHandler() => new ConsoleReader(CreateDisplayMessages());
        public static ICalculator CreateCalculator(decimal taxRate, decimal discountRate, DiscountCombinationType type)
        {
            return new Calculator(taxRate, discountRate,  CreateDiscountService(), type,2);
        }
        public static IPrinter CreatePrinter(ICalculator calculator, string currency)
            => new Printer(calculator, CreateExpenseService(), CreateCapService(), currency);




    }
}
