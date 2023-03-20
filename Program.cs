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
            var messenger = Factory.CreateDisplayMessages();
            var inputHandler = Factory.CreateConsoleInputHandler();
            decimal taxRate = GetTaxRate(messenger, inputHandler);
            decimal discountRate = GetDiscountRate(messenger, inputHandler);
            int precision = GetPrecision(messenger, inputHandler);
            GetCustomProductDiscount(messenger, inputHandler);
            GetProductDiscountCaps(messenger, inputHandler, precision);
            GetProductExpenses(messenger, inputHandler, precision);
            Report(messenger, inputHandler, taxRate, discountRate, precision);
            messenger.DisplayExitMessage();
        }

        private static void Report(IDisplayMessages messenger, IConsoleInputHandler inputHandler, decimal taxRate, decimal discountRate, int precision)
        {
            messenger.DisplayOrderOfCalculationsMessage();
            var reportGenerator = Factory.CreateReportGenerator();
            if (inputHandler.DemandsDiscountBeforeTax())
            {
                reportGenerator.GenerateReport(taxRate, discountRate, true, precision);
            }
            else
            {
                reportGenerator.GenerateReport(taxRate, discountRate, false, precision);
            }
        }

        private static void GetProductExpenses(IDisplayMessages messenger, IConsoleInputHandler inputHandler, int precision)
        {
            messenger.DisplayAddExpensesMessage();
            if (inputHandler.IsYes())
            {
                inputHandler.GetCustomExpenses(precision);
            }
        }

        private static void GetProductDiscountCaps(IDisplayMessages messenger, IConsoleInputHandler inputHandler, int precision)
        {
            messenger.DisplayDemandsDiscountCapMessage();
            if (inputHandler.IsYes())
            {
                inputHandler.GetCustomDiscountCap(precision);
            }
        }

        private static void GetCustomProductDiscount(IDisplayMessages messenger, IConsoleInputHandler inputHandler)
        {
            messenger.DisplayCustomDiscountMessage();
            if (inputHandler.DemandsCustomDiscount())
            {
                inputHandler.GetCustomDiscounts();
            }
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
            if (inputHandler.IsYes())
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
