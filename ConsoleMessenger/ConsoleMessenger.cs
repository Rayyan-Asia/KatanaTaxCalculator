using KatanaTaxCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_Kata
{
    public class ConsoleMessenger : IConsoleMessenger
    {
        public void DemandDefaultTaxRateMessage()
        {
            Console.WriteLine("Would you like a custom tax rate?\nenter 'y' as yes or anything else as no" +
                "\n20 percent will be the default tax rate");
        }
        public void DemandTaxRateMessage()
        {
            Console.WriteLine("Please enter the tax rate you wish to have." +
                "\nBe sure to give a number from 0 to 100 ;)");
        }

        public void DemandDiscountRateMessage()
        {
            Console.WriteLine("Please enter the discount rate you wish to have." +
                "\nBe sure to give a number from 0 to 100 ;)");
        }

        public void SumOrMultiplicativeDiscountMessage()
        {
            Console.WriteLine("Would you like the discounts to be simple sum\nEnter 'y' for yes or any other key for multiplicative discounts.");
        }
        public void ErrorMessage(string input)
        {
            Console.WriteLine($"The given input '{input}' is not an acceptable input.");
        }

        public void ExitMessage()
        {
            Console.WriteLine("Thank you for using our program ;)");
        }

        public void DemandsDiscountCapMessage()
        {
            Console.WriteLine("Would you like to enter discount caps?\nenter 'y' for yes and anything else as no");
        }

        public void DemandPrecisionMeasurementMessage()
        {
            Console.WriteLine("Please enter the precision you desire");
        }

        public void DemandCurrencyMessage()
        {
            Console.WriteLine("Please enter the currency you want the system to use.\nThe Currency must be 3 characters long");
        }
    }
}
