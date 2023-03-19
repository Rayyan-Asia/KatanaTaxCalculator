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
            messenger.DisplayCustomDiscountMessage();
            var repository = Factory.CreateProductRepository();
            if (DemandsCustomDiscount())
            {
                GetCustomDiscounts(repository, messenger);
            }
            messenger.DisplayAddExpensesMessage();
            if (DemandsAddingExpenses())
            {
                GetCustomExpenses(repository, messenger);
            }
            messenger.DisplayOrderOfCalculationsMessage();
            if (DemandsDiscountBeforeTax(messenger))
            {
                IterateAndReport(repository, taxRate, discountRate, true, messenger);
            }
            else
            {
                IterateAndReport(repository, taxRate, discountRate, false, messenger);
            }

            messenger.DisplayExitMessage();
        }

        private static void GetCustomExpenses(IProductRepository repository, IDisplayMessages messenger)
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
                        GetExpenseValueAndDescription(product, messenger);
                    }
                }

            }
        }

        private static void GetExpenseValueAndDescription(IProduct product, IDisplayMessages messenger)
        {
            var input = "";
            while (input != "-1")
            {
                messenger.DisplayIsExpensePercentageMessage();
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
                    var value = ParseDouble(messenger, input);
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

        private static bool DemandsAddingExpenses()
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
            if (input == "2")
            {
                return true;
            }
            else if (input == "1")
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

        private static double ParseDouble(IDisplayMessages messenger, string input)
        {
            var upc = 0.0;
            try { upc = Math.Round(double.Parse(input), 2); }
            catch (Exception ex)
            {
                messenger.DisplayErrorMessage(input);
                messenger.DisplayExitMessage();
                Environment.Exit(-1);
            }

            return upc;
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



        private static void IterateAndReport(IProductRepository repository, decimal taxRate, decimal discountRate, bool discountfirst, IDisplayMessages messenger)
        {
            var priceCalculator = Factory.CreateTaxCalculator();
            var logger = Factory.CreateLogger();
            var products = repository.ListProducts();
            foreach (IProduct product in products)
            {
                if (discountfirst)
                {
                    ReportDiscountFirst(taxRate, discountRate, priceCalculator, logger, product);
                }
                else
                {
                    messenger.DisplaySumOrMultiplicativeDiscountMessage();
                    var input = Console.ReadLine().Replace(" ", "").ToLower();
                    var priceAfterTax = priceCalculator.CalculatePriceWithGivenTaxRate(product, taxRate);
                    var priceAfterDiscount = 0.0;
                    bool multiplicative = false;
                    if (input == "y")
                    {
                        priceAfterDiscount = priceCalculator.CalculatePriceWithGivenDiscountRateAfterTax(product,priceAfterTax ,discountRate); 
                    }
                    else
                    {
                        priceAfterDiscount = priceCalculator.CalculatePriceWithMultiplicativeDiscount(product, priceAfterTax, discountRate);
                        multiplicative = true;
                    }
                    ReportDiscountAfter(taxRate, discountRate, priceCalculator, logger, product, priceAfterTax, priceAfterDiscount,multiplicative);
                }

            }

        }

        private static void ReportDiscountAfter(decimal taxRate, decimal discountRate, IProductPriceCalculator priceCalculator, IProductReportLogger logger, IProduct product, double priceAfterTax, double priceAfterDiscount, bool multiplicative)
        {
            logger.PrintProduct(product);
            logger.PrintTax(product.Price, priceAfterTax, taxRate);
            if (!multiplicative)
            {
                logger.PrintDiscount(priceAfterTax, priceAfterDiscount, discountRate + product.Discount);
            }
            else
            {
                logger.PrintMultiplicativeDiscount(priceAfterTax, priceAfterDiscount, product.Discount, discountRate);
            }
            double totalExpenses = HandleExpenses(priceCalculator, logger, product);
            logger.PrintFinalPrice(priceAfterDiscount + totalExpenses);
        }

        private static void ReportDiscountFirst(decimal taxRate, decimal discountRate, IProductPriceCalculator priceCalculator, IProductReportLogger logger, IProduct product)
        {
            var priceAfterDiscount = priceCalculator.CalculatePriceWithGivenDiscountRate(product);
            var priceAfterTax = priceCalculator.CalculatePriceWithGivenTaxRateAfterDiscount(priceAfterDiscount, taxRate);
            var priceAfterSecondDiscount = priceCalculator.CalculatePriceWithGivenDiscountRateAfterTaxWithoutUPC(product, priceAfterTax, discountRate);
            logger.PrintProduct(product);
            if (product.Discount != 0) { logger.PrintDiscount(product.Price, priceAfterDiscount, product.Discount); }
            logger.PrintTax(priceAfterDiscount, priceAfterTax, taxRate);
            logger.PrintDiscount(priceAfterTax, priceAfterSecondDiscount, discountRate);
            double totalExpenses = HandleExpenses(priceCalculator, logger, product);
            logger.PrintFinalPrice(priceAfterSecondDiscount + totalExpenses);
        }

        private static double HandleExpenses(IProductPriceCalculator priceCalculator, IProductReportLogger logger, IProduct product)
        {
            var totalExpenses = 0.0;
            foreach (Expense expense in product.Expenses)
            {
                var cost = priceCalculator.CalculateExpense(expense, product);
                logger.PrintExpense(expense.Description, cost);
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
