using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatanaTaxCalculator;
using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public class ConsoleReader : IConsoleReader
    {
        private readonly IConsoleMessenger _messenger;
        public ConsoleReader(IConsoleMessenger messenger)
        {
            this._messenger = messenger;
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

        public string ReadCurrency()
        {
            var input = Console.ReadLine().ToUpper().Replace(" ",""); 
            if (input.Length == 3)
            {
                return input;
            }
            else
            {
                ExitFromInvalidInput(input);
            }
            return "";
        }
       
        public bool UserEnteredYes()
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
      

        public void ExitFromInvalidInput(string input)
        {
            _messenger.ErrorMessage(input);
            _messenger.ExitMessage();
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
