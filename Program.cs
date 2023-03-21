using Katana_Tax_Calculator;
using KatanaTaxCalculator.KatanaTaxCalculator;
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
        public static void Main()
        {
            var messenger = Factory.CreateDisplayMessages();
            var inputHandler = Factory.CreateConsoleInputHandler();
            ReadCalculatorConfigurations(messenger, inputHandler);
            CalculateAndReport();
            messenger.ExitMessage();
        }

        private static void ReadCalculatorConfigurations(IConsoleMessenger messenger, IConsoleInputHandler inputHandler)
        {
            decimal taxRate = ReadTaxRate(messenger, inputHandler);
            decimal discountRate = ReadDiscountRate(messenger, inputHandler);
            DiscountCombinationType type = IsMultiplicative(messenger, inputHandler);
            string currency = ReadCurrency(messenger, inputHandler);
            
        }

        private static DiscountCombinationType IsMultiplicative(IConsoleMessenger messenger, IConsoleInputHandler inputHandler)
        {
            messenger.SumOrMultiplicativeDiscountMessage();
            if(inputHandler.UserEnteredYes())
            {
                return DiscountCombinationType.Additive;
            }
            return DiscountCombinationType.Multiplicative;
        }

        private static void CalculateAndReport()
        {
            throw new NotImplementedException();
        }

        private static string ReadCurrency(IConsoleMessenger messenger, IConsoleInputHandler inputHandler)
        {
            messenger.DemandCurrencyMessage();
            string currency = inputHandler.ReadCurrency();
            return currency;
        }

        private static decimal ReadDiscountRate(IConsoleMessenger messenger, IConsoleInputHandler inputHandler)
        {
            messenger.DemandDiscountRateMessage();
            decimal discountRate = inputHandler.ParseDecimal();
            return discountRate;
        }
        private static decimal ReadTaxRate(IConsoleMessenger messenger, IConsoleInputHandler inputHandler)
        {
            messenger.DemandDefaultTaxRateMessage();
            decimal taxRate;
            if (inputHandler.UserEnteredYes())
            {
                messenger.DemandTaxRateMessage();
                taxRate = inputHandler.ParseDecimal();
            }
            else
            {
                taxRate = 0.2M; 
            }
            return taxRate;
        }
    }
}
