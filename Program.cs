using Katana_Tax_Calculator;
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
            /* Things to change
             * additive or multipicative
             * general discount
             * general taxRate
             * currency
            */
            var messenger = Factory.CreateDisplayMessages();
            var inputHandler = Factory.CreateConsoleInputHandler();
            decimal taxRate = GetTaxRate(messenger, inputHandler);
            decimal discountRate = GetDiscountRate(messenger, inputHandler);
            int precision = GetPrecision(messenger, inputHandler);
            messenger.DisplayExitMessage();
        }
        private static int GetPrecision(IDisplayMessages messenger, IConsoleInputHandler inputHandler)
        {
            messenger.DisplayDemandPrecisionMeasurementMessage();
            int precision = inputHandler.ParseInt(Console.ReadLine().Replace(" ", "").ToLower());
            return precision;
        }

        private static decimal GetDiscountRate(IDisplayMessages messenger, IConsoleInputHandler inputHandler)
        {
            messenger.DisplayDiscountMessage();
            decimal discountRate = inputHandler.ParseDecimal();
            return discountRate;
        }

        private static decimal GetTaxRate(IDisplayMessages messenger, IConsoleInputHandler inputHandler)
        {
            messenger.DisplayDemandDefaultTaxRateMessage();
            decimal taxRate;
            if (inputHandler.UserEnteredYes())
            {
                messenger.DisplayDemandTaxRateMessage();
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
