using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace Price_Calculator_Kata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var messenger = Factory.CreateDisplayMessages();
            messenger.DisplayWelcomeMessage();
            decimal taxRate = FilterInput(messenger);
            messenger.DisplayDiscountMessage();
            decimal discountRate = FilterInput(messenger);
            messenger.DisplayDemandPrecisionMeasurement();
            int precision = ParseInt(messenger,Console.ReadLine().Replace(" ","").ToLower());
            messenger.DisplayCustomDiscountMessage();
            var repository = Factory.CreateProductRepository();
            if (DemandsCustomDiscount())
            {
                GetCustomDiscounts(repository, messenger);
            }
            messenger.DisplayDemandsDiscountCap();
            if (IsYes())
            {
                GetCustomDiscountCap(repository, messenger,precision);
            }
            messenger.DisplayAddExpensesMessage();
            if (IsYes())
            {
                GetCustomExpenses(repository, messenger,precision);
            }
            messenger.DisplayOrderOfCalculationsMessage();
            if (DemandsDiscountBeforeTax(messenger))
            {
                IterateAndReport(repository, taxRate, discountRate, true, messenger,precision);
            }
            else
            {
                IterateAndReport(repository, taxRate, discountRate, false, messenger, precision);
            }

            messenger.DisplayExitMessage();
        }

        private static void GetCustomDiscountCap(IProductRepository repository, IDisplayMessages messenger, int precision)
        {
            var input = "";
            while (input != "-1")
            {
                messenger.DisplayDemandUpcForDiscountCap();
                input = Console.ReadLine().Replace(" ", "");
                if (input != "-1")
                {
                    var upc = ParseInt(messenger, input);
                    var product = repository.GetProductByUPC(upc);
                    if (product == null)
                    {
                        messenger.DisplayProductNotFoundMessage();
                    }
                    else
                    {
                        GetDiscountCap(product, messenger, precision);
                    }
                }

            }
        }

        private static void GetDiscountCap(IProduct product, IDisplayMessages messenger, int precision)
        {
            messenger.DisplayIsPricePercentageMessage();
            if (IsYes())
            {
                messenger.DisplayDemandDiscountCapPercentage();
                var input = Console.ReadLine().Replace(" ", "").ToLower();
                var percentage = ParsePercentage(input);
                var fraction = Math.Round((percentage / 100.0) * product.Price, precision);
                product.DiscountCap = fraction;
            }
            else
            {
                messenger.DisplayDemandDiscountCapAmount();
                var input = Console.ReadLine().Replace(" ", "").ToLower();
                var amount = ParseDouble(messenger, input, precision);
                amount = Math.Round(amount, precision);
                product.DiscountCap = amount;
            }
        }

        private static void GetCustomExpenses(IProductRepository repository, IDisplayMessages messenger, int precision)
        {
            var input = "";
            while (input != "-1")
            {
                messenger.DisplayDemandUpcForExpenseMessage();
                input = Console.ReadLine().Replace(" ", "");
                if (input != "-1")
                {
                    var upc = ParseInt(messenger, input);
                    var product = repository.GetProductByUPC(upc);
                    if (product == null)
                    {
                        messenger.DisplayProductNotFoundMessage();
                    }
                    else
                    {
                        GetExpenseValueAndDescription(product, messenger, precision);
                    }
                }

            }
        }

        private static void GetExpenseValueAndDescription(IProduct product, IDisplayMessages messenger, int precision)
        {
            var input = "";
            while (input != "-1")
            {
                messenger.DisplayIsPricePercentageMessage();
                input = Console.ReadLine().Replace(" ", "");
                if (input == "y")
                {
                    messenger.DisplayExpensePercentageMessage();
                    input = Console.ReadLine().ToLower();
                    var percentage = ParsePercentage(input);
                    messenger.DisplayExpenseDescriptionMessage();
                    var description = Console.ReadLine();
                    var expense = Factory.CreateExpense();
                    expense.IsPercentage = true;
                    expense.Description = description;
                    expense.Amount = (double)percentage;
                    product.Expenses.Add(expense);
                }
                else
                {
                    messenger.DisplayExpenseAmountMessage();
                    input = Console.ReadLine().ToLower();
                    var value = ParseDouble(messenger, input,precision);
                    messenger.DisplayExpenseDescriptionMessage();
                    var description = Console.ReadLine();
                    var expense = Factory.CreateExpense();
                    expense.IsPercentage = false;
                    expense.Description = description;
                    expense.Amount = value;
                    product.Expenses.Add(expense);
                }
                messenger.DisplayWantsToExitMessage();
                input = Console.ReadLine().ToLower();
            }
        }

        private static bool IsYes()
        {
            var input = Console.ReadLine().Replace(" ", "");
            if (input == "y")
            {
                return true;
            }
            return false;
        }

        private static bool DemandsDiscountBeforeTax(IDisplayMessages messenger)
        {
            var input = Console.ReadLine().Replace(" ", "");
            if (input == "1")
            {
                return true;
            }
            else if (input == "2")
            {
                return false;
            }
            ExitFromInvalidInput(input, messenger);
            return true;
        }

        private static void GetCustomDiscounts(IProductRepository repository, IDisplayMessages messenger)
        {
            var input = "";
            while (input != "-1")
            {
                messenger.DisplayDemandProductUpcMessage();
                input = Console.ReadLine();
                if (input.Replace(" ", "") != "-1")
                {
                    var upc = ParseInt(messenger, input);
                    var product = repository.GetProductByUPC(upc);
                    if (product == null)
                    {
                        messenger.DisplayProductNotFoundMessage();
                    }
                    else
                    {
                        messenger.DisplayDiscountMessage();
                        product.Discount = FilterInput(messenger);
                    }
                }

            }
        }

        private static int ParseInt(IDisplayMessages messenger, string input)
        {
            var upc = 0;
            try { upc = int.Parse(input); }
            catch (Exception ex)
            {
                messenger.DisplayErrorMessage(input);
                messenger.DisplayExitMessage();
                Environment.Exit(-1);
            }

            return upc;
        }

        private static double ParseDouble(IDisplayMessages messenger, string input, int precision)
        {
            var amount = 0.0;
            try { amount = Math.Round(double.Parse(input),precision); }
            catch (Exception ex)
            {
                messenger.DisplayErrorMessage(input);
                messenger.DisplayExitMessage();
                Environment.Exit(-1);
            }

            return amount;
        }

        private static bool DemandsCustomDiscount()
        {
            var input = Console.ReadLine();
            if (input.Replace(" ", "").ToLower() == "y")
            {
                return true;
            }
            return false;
        }

        private static decimal FilterInput(IDisplayMessages messenger)
        {
            var input = Console.ReadLine();
            var number = ParsePercentage(input);
            ExitIfInvalid(number, input, messenger);
            return (decimal)(number / 100.0);
        }



        private static int ParsePercentage(string input)
        {

            if (input == null) return -1;
            if (input.Length == 0) return -1;
            var percentage = -1;
            try
            {
                var parsedInt = int.Parse(input);
                if (parsedInt < 0 || parsedInt > 100) return -1;
                percentage = parsedInt;
            }
            catch (Exception ex)
            {
                return -1;
            }
            return percentage;
        }



        private static void IterateAndReport(IProductRepository repository, decimal taxRate, decimal discountRate, bool discountfirst, IDisplayMessages messenger, int precision)
        {
            var priceCalculator = Factory.CreateTaxCalculator();
            var logger = Factory.CreateLogger();
            var products = repository.ListProducts();
            foreach (IProduct product in products)
            {
                if (discountfirst)
                {
                    ReportDiscountFirst(taxRate, discountRate, priceCalculator, logger, product, precision);
                }
                else
                {
                    messenger.DisplaySumOrMultiplicativeDiscountMessage();
                    var input = Console.ReadLine().Replace(" ", "").ToLower();
                    var priceAfterTax = priceCalculator.CalculatePriceWithGivenTaxRate(product, taxRate, precision);
                    var priceAfterDiscount = 0.0;
                    bool multiplicative = false;
                    if (input == "y")
                    {
                        priceAfterDiscount = priceCalculator.CalculatePriceWithGivenDiscountRateAfterTax(product, priceAfterTax, discountRate,precision);
                    }
                    else
                    {
                        priceAfterDiscount = priceCalculator.CalculatePriceWithMultiplicativeDiscount(product, priceAfterTax, discountRate,precision);
                        multiplicative = true;
                    }
                    if (product.DiscountCap != 0 && priceAfterTax - priceAfterDiscount > product.DiscountCap)
                    {
                        priceAfterDiscount = priceAfterTax - product.DiscountCap;
                        logger.PrintProduct(product, precision); 
                        logger.PrintTax(product.Price, priceAfterTax, taxRate, product.Currency, precision);
                        logger.PrintCapDeduction(priceAfterTax, priceAfterDiscount,product.Currency, precision);
                        double totalExpenses = HandleExpenses(priceCalculator, logger, product, precision);
                        logger.PrintFinalPrice(priceAfterDiscount + totalExpenses,product.Currency, precision);
                    }
                    else
                    {
                        ReportDiscountAfter(taxRate, discountRate, priceCalculator, logger, product, priceAfterTax, priceAfterDiscount, multiplicative, precision);
                    }


                }

            }

        }

        private static void ReportDiscountAfter(decimal taxRate, decimal discountRate, IProductPriceCalculator priceCalculator, IProductReportLogger logger, IProduct product, double priceAfterTax, double priceAfterDiscount, bool multiplicative, int precision)
        {
            logger.PrintProduct(product, precision);
            logger.PrintTax(product.Price, priceAfterTax, taxRate, product.Currency, precision);
            if (!multiplicative)
            {
                logger.PrintDiscount(priceAfterTax, priceAfterDiscount, discountRate + product.Discount, product.Currency, precision);
            }
            else
            {
                logger.PrintMultiplicativeDiscount(priceAfterTax, priceAfterDiscount, product.Discount, discountRate,product.Currency, precision);
            }
            double totalExpenses = HandleExpenses(priceCalculator, logger, product, precision);
            logger.PrintFinalPrice(priceAfterDiscount + totalExpenses, product.Currency, precision);
        }

        private static void ReportDiscountFirst(decimal taxRate, decimal discountRate, IProductPriceCalculator priceCalculator, IProductReportLogger logger, IProduct product, int precision)
        {
            var priceAfterDiscount = priceCalculator.CalculatePriceWithGivenDiscountRate(product,precision);
            var cap = product.DiscountCap;
            if (cap == 0)
            {
                var priceAfterTax = priceCalculator.CalculatePriceWithGivenTaxRateAfterDiscount(priceAfterDiscount, taxRate,precision);
                var priceAfterSecondDiscount = priceCalculator.CalculatePriceWithGivenDiscountRateAfterTaxWithoutUPC(product, priceAfterTax, discountRate,precision);
                logger.PrintProduct(product, precision);
                if (product.Discount != 0) { logger.PrintDiscount(product.Price, priceAfterDiscount, product.Discount, product.Currency, precision); }
                logger.PrintTax(priceAfterDiscount, priceAfterTax, taxRate, product.Currency, precision);
                logger.PrintDiscount(priceAfterTax, priceAfterSecondDiscount, discountRate, product.Currency, precision);
                double totalExpenses = HandleExpenses(priceCalculator, logger, product, precision);
                logger.PrintFinalPrice(priceAfterSecondDiscount + totalExpenses, product.Currency, precision);
            }
            else
            {
                cap -= product.Price - priceAfterDiscount;
                if (cap < 0)
                {
                    priceAfterDiscount = product.Price - product.DiscountCap;
                    var priceAfterTax = priceCalculator.CalculatePriceWithGivenTaxRateAfterDiscount(priceAfterDiscount, taxRate, precision);
                    logger.PrintProduct(product, precision);
                    logger.PrintCapDeduction(product.Price, priceAfterDiscount, product.Currency, precision);
                    logger.PrintTax(priceAfterDiscount, priceAfterTax, taxRate, product.Currency, precision);
                    double totalExpenses = HandleExpenses(priceCalculator, logger, product, precision);
                    logger.PrintFinalPrice(priceAfterTax + totalExpenses, product.Currency, precision);
                }
                else
                {
                    var priceAfterTax = priceCalculator.CalculatePriceWithGivenTaxRateAfterDiscount(priceAfterDiscount, taxRate,precision);
                    var priceAfterSecondDiscount = priceCalculator.CalculatePriceWithGivenDiscountRateAfterTaxWithoutUPC(product, priceAfterTax, discountRate,precision);
                    if (cap < priceAfterTax - priceAfterSecondDiscount)
                    {
                        priceAfterSecondDiscount = priceAfterTax - cap;
                        logger.PrintProduct(product, precision);
                        logger.PrintDiscount(product.Price, priceAfterDiscount, discountRate, product.Currency, precision);
                        logger.PrintTax(priceAfterDiscount, priceAfterTax, taxRate, product.Currency, precision);
                        logger.PrintCapDeduction(priceAfterTax, priceAfterSecondDiscount, product.Currency, precision);
                        double totalExpenses = HandleExpenses(priceCalculator, logger, product,precision);
                        logger.PrintFinalPrice(priceAfterSecondDiscount + totalExpenses, product.Currency, precision);
                    }
                    else
                    {
                        logger.PrintProduct(product, precision);
                        if (product.Discount != 0) { logger.PrintDiscount(product.Price, priceAfterDiscount, product.Discount, product.Currency, precision); }
                        logger.PrintTax(priceAfterDiscount, priceAfterTax, taxRate, product.Currency, precision);
                        logger.PrintDiscount(priceAfterTax, priceAfterSecondDiscount, discountRate, product.Currency, precision);
                        double totalExpenses = HandleExpenses(priceCalculator, logger, product,precision);
                        logger.PrintFinalPrice(priceAfterSecondDiscount + totalExpenses, product.Currency, precision);
                    }
                }
            }
        }

        private static double HandleExpenses(IProductPriceCalculator priceCalculator, IProductReportLogger logger, IProduct product, int precision)
        {
            var totalExpenses = 0.0;
            foreach (Expense expense in product.Expenses)
            {
                var cost = priceCalculator.CalculateExpense(expense, product, precision);
                logger.PrintExpense(expense.Description, cost, product.Currency, precision);
                totalExpenses += cost;
            }

            return totalExpenses;
        }

        private static void ExitIfInvalid(int number, string input, IDisplayMessages messenger)
        {
            if (number == -1) // if input is invalid
            {
                ExitFromInvalidInput(input, messenger);
            }
        }

        private static void ExitFromInvalidInput(string input, IDisplayMessages messenger)
        {
            messenger.DisplayErrorMessage(input);
            messenger.DisplayExitMessage();
            Environment.Exit(-1);
        }
    }
}
