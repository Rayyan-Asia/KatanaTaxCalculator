using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public class InputHandler : IConsoleInputHandler
    {
        
        public IProductRepository repository { get ; set; }
        public IDisplayMessages messenger { get; set; }
        public InputHandler(IProductRepository repository, IDisplayMessages messenger)
        {
            this.repository = repository;
            this.messenger = messenger;
        }

        public bool DemandsDiscountBeforeTax()
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
            ExitFromInvalidInput(input);
            return true;
        }
        public bool DemandsCustomDiscount()
        {
            var input = Console.ReadLine();
            if (input.Replace(" ", "").ToLower() == "y")
            {
                return true;
            }
            return false;
        }
        public bool IsYes()
        {
            var input = Console.ReadLine().Replace(" ", "").ToLower();
            if (input == "y")
            {
                return true;
            }
            return false;
        }
        public decimal ParseDecimal()
        {
            var input = Console.ReadLine();
            var number = ParsePercentage(input);
            ExitIfInvalid(number, input);
            return (decimal)(number / 100.0);
        }

        public int ParsePercentage(string input)
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

        public int ParseInt( string input)
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

        public double ParseDouble( string input, int precision)
        {
            var amount = 0.0;
            try { amount = Math.Round(double.Parse(input), precision); }
            catch (Exception ex)
            {
                messenger.DisplayErrorMessage(input);
                messenger.DisplayExitMessage();
                Environment.Exit(-1);
            }

            return amount;
        }

        public void GetCustomDiscounts()
        {
            var input = "";
            while (input != "-1")
            {
                messenger.DisplayDemandProductUpcMessage();
                input = Console.ReadLine();
                if (input.Replace(" ", "") != "-1")
                {
                    var upc = ParseInt( input);
                    var product = repository.GetProductByUPC(upc);
                    if (product == null)
                    {
                        messenger.DisplayProductNotFoundMessage();
                    }
                    else
                    {
                        messenger.DisplayDiscountMessage();
                        product.Discount = ParseDecimal();
                    }
                }

            }
        }
        public void GetCustomExpenses(int precision)
        {
            var input = "";
            while (input != "-1")
            {
                messenger.DisplayDemandUpcForExpenseMessage();
                input = Console.ReadLine().Replace(" ", "");
                if (input != "-1")
                {
                    var upc = ParseInt( input);
                    var product = repository.GetProductByUPC(upc);
                    if (product == null)
                    {
                        messenger.DisplayProductNotFoundMessage();
                    }
                    else
                    {
                        GetExpenseValueAndDescription(product, precision);
                    }
                }

            }
        }

        public void GetCustomDiscountCap(int precision)
        {
            var input = "";
            while (input != "-1")
            {
                messenger.DisplayDemandUpcForDiscountCapMessage();
                input = Console.ReadLine().Replace(" ", "");
                if (input != "-1")
                {
                    var upc = ParseInt(input);
                    var product = repository.GetProductByUPC(upc);
                    if (product == null)
                    {
                        messenger.DisplayProductNotFoundMessage();
                    }
                    else
                    {
                        GetDiscountCap(product, precision);
                    }
                }

            }
        }

        public void GetDiscountCap(IProduct product, int precision)
        {
            messenger.DisplayIsPricePercentageMessage();
            if (IsYes())
            {
                messenger.DisplayDemandDiscountCapPercentageMessage();
                var input = Console.ReadLine().Replace(" ", "").ToLower();
                var percentage = ParsePercentage(input);
                var fraction = Math.Round((percentage / 100.0) * product.Price, precision);
                product.DiscountCap = fraction;
            }
            else
            {
                messenger.DisplayDemandDiscountCapAmountMessage();
                var input = Console.ReadLine().Replace(" ", "").ToLower();
                var amount = ParseDouble(input, precision);
                product.DiscountCap = amount;
            }
        }
        public void GetExpenseValueAndDescription(IProduct product, int precision)
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
                    var value = ParseDouble(input, precision);
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
        public void ExitFromInvalidInput(string input)
        {
            messenger.DisplayErrorMessage(input);
            messenger.DisplayExitMessage();
            Environment.Exit(-1);
        }
        private void ExitIfInvalid(int number, string input)
        {
            if (number == -1) // if input is invalid
            {
                ExitFromInvalidInput(input);
            }
        }
    }
}
