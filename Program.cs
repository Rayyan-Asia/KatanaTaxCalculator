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
            messenger.DisplayDemandsDiscountCap();
            if (IsYes())
            {
                GetCustomDiscountCap(repository, messenger);
            }
            messenger.DisplayAddExpensesMessage();
            if (IsYes())
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

        private static void GetCustomDiscountCap(IProductRepository repository, IDisplayMessages messenger)
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
                        GetDiscountCap(product, messenger);
                    }
                }

            }
        }

        private static void GetDiscountCap(IProduct product, IDisplayMessages messenger)
        {
            messenger.DisplayIsPricePercentageMessage();
            if (IsYes())
            {
                messenger.DisplayDemandDiscountCapPercentage();
                var input = Console.ReadLine().Replace(" ", "").ToLower();
                var percentage = ParsePercentage(input);
                var fraction = Math.Round((percentage / 100.0) * product.Price, 2);
                product.DiscountCap = fraction;
            }
            else
            {
                messenger.DisplayDemandDiscountCapAmount();
                var input = Console.ReadLine().Replace(" ", "").ToLower();
                var amount = ParseDouble(messenger, input);
                amount = Math.Round(amount, 2);
                product.DiscountCap = amount;
            }
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
            var amount = 0.0;
            try { amount = Math.Round(double.Parse(input), 2); }
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
                        priceAfterDiscount = priceCalculator.CalculatePriceWithGivenDiscountRateAfterTax(product, priceAfterTax, discountRate);
                    }
                    else
                    {
                        priceAfterDiscount = priceCalculator.CalculatePriceWithMultiplicativeDiscount(product, priceAfterTax, discountRate);
                        multiplicative = true;
                    }
                    if (product.DiscountCap != 0 && priceAfterTax - priceAfterDiscount > product.DiscountCap)
                    {
                        priceAfterDiscount = priceAfterTax - product.DiscountCap;
                        logger.PrintProduct(product);
                        logger.PrintTax(product.Price, priceAfterTax, taxRate, product.Currency);
                        logger.PrintCapDeduction(priceAfterTax, priceAfterDiscount,product.Currency);
                        double totalExpenses = HandleExpenses(priceCalculator, logger, product);
                        logger.PrintFinalPrice(priceAfterDiscount + totalExpenses,product.Currency);
                    }
                    else
                    {
                        ReportDiscountAfter(taxRate, discountRate, priceCalculator, logger, product, priceAfterTax, priceAfterDiscount, multiplicative);
                    }


                }

            }

        }

        private static void ReportDiscountAfter(decimal taxRate, decimal discountRate, IProductPriceCalculator priceCalculator, IProductReportLogger logger, IProduct product, double priceAfterTax, double priceAfterDiscount, bool multiplicative)
        {
            logger.PrintProduct(product);
            logger.PrintTax(product.Price, priceAfterTax, taxRate, product.Currency);
            if (!multiplicative)
            {
                logger.PrintDiscount(priceAfterTax, priceAfterDiscount, discountRate + product.Discount, product.Currency);
            }
            else
            {
                logger.PrintMultiplicativeDiscount(priceAfterTax, priceAfterDiscount, product.Discount, discountRate,product.Currency);
            }
            double totalExpenses = HandleExpenses(priceCalculator, logger, product);
            logger.PrintFinalPrice(priceAfterDiscount + totalExpenses, product.Currency);
        }

        private static void ReportDiscountFirst(decimal taxRate, decimal discountRate, IProductPriceCalculator priceCalculator, IProductReportLogger logger, IProduct product)
        {
            var priceAfterDiscount = priceCalculator.CalculatePriceWithGivenDiscountRate(product);
            var cap = product.DiscountCap;
            if (cap == 0)
            {
                var priceAfterTax = priceCalculator.CalculatePriceWithGivenTaxRateAfterDiscount(priceAfterDiscount, taxRate);
                var priceAfterSecondDiscount = priceCalculator.CalculatePriceWithGivenDiscountRateAfterTaxWithoutUPC(product, priceAfterTax, discountRate);
                logger.PrintProduct(product);
                if (product.Discount != 0) { logger.PrintDiscount(product.Price, priceAfterDiscount, product.Discount, product.Currency); }
                logger.PrintTax(priceAfterDiscount, priceAfterTax, taxRate, product.Currency);
                logger.PrintDiscount(priceAfterTax, priceAfterSecondDiscount, discountRate, product.Currency);
                double totalExpenses = HandleExpenses(priceCalculator, logger, product);
                logger.PrintFinalPrice(priceAfterSecondDiscount + totalExpenses, product.Currency);
            }
            else
            {
                cap -= product.Price - priceAfterDiscount;
                if (cap < 0)
                {
                    priceAfterDiscount = product.Price - product.DiscountCap;
                    var priceAfterTax = priceCalculator.CalculatePriceWithGivenTaxRateAfterDiscount(priceAfterDiscount, taxRate);
                    logger.PrintProduct(product);
                    logger.PrintCapDeduction(product.Price, priceAfterDiscount, product.Currency);
                    logger.PrintTax(priceAfterDiscount, priceAfterTax, taxRate, product.Currency);
                    double totalExpenses = HandleExpenses(priceCalculator, logger, product);
                    logger.PrintFinalPrice(priceAfterTax + totalExpenses, product.Currency);
                }
                else
                {
                    var priceAfterTax = priceCalculator.CalculatePriceWithGivenTaxRateAfterDiscount(priceAfterDiscount, taxRate);
                    var priceAfterSecondDiscount = priceCalculator.CalculatePriceWithGivenDiscountRateAfterTaxWithoutUPC(product, priceAfterTax, discountRate);
                    if (cap < priceAfterTax - priceAfterSecondDiscount)
                    {
                        priceAfterSecondDiscount = priceAfterTax - cap;
                        logger.PrintProduct(product);
                        logger.PrintDiscount(product.Price, priceAfterDiscount, discountRate, product.Currency);
                        logger.PrintTax(priceAfterDiscount, priceAfterTax, taxRate, product.Currency);
                        logger.PrintCapDeduction(priceAfterTax, priceAfterSecondDiscount, product.Currency);
                        double totalExpenses = HandleExpenses(priceCalculator, logger, product);
                        logger.PrintFinalPrice(priceAfterSecondDiscount + totalExpenses, product.Currency);
                    }
                    else
                    {
                        logger.PrintProduct(product);
                        if (product.Discount != 0) { logger.PrintDiscount(product.Price, priceAfterDiscount, product.Discount, product.Currency); }
                        logger.PrintTax(priceAfterDiscount, priceAfterTax, taxRate, product.Currency);
                        logger.PrintDiscount(priceAfterTax, priceAfterSecondDiscount, discountRate, product.Currency);
                        double totalExpenses = HandleExpenses(priceCalculator, logger, product);
                        logger.PrintFinalPrice(priceAfterSecondDiscount + totalExpenses, product.Currency);
                    }
                }
            }
        }

        private static double HandleExpenses(IProductPriceCalculator priceCalculator, IProductReportLogger logger, IProduct product)
        {
            var totalExpenses = 0.0;
            foreach (Expense expense in product.Expenses)
            {
                var cost = priceCalculator.CalculateExpense(expense, product);
                logger.PrintExpense(expense.Description, cost, product.Currency);
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
