using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_Kata
{
    public class DisplayMessages : IDisplayMessages
    {
        public void DisplayWelcomeMessage()
        {
            Console.WriteLine("Please enter the tax rate you wish to have." +
                "\nBe sure to give a number from 0 to 100 ;)");
        }

        public void DisplayErrorMessage(string input)
        {
            Console.WriteLine($"The given input '{input}' is not an acceptable input.");
        }

        public void DisplayExitMessage()
        {
            Console.WriteLine("Thank you for using our program ;)");
        }

        public void DisplayDiscountMessage()
        {
            Console.WriteLine("Please enter the discount rate you wish to have." +
                "\nBe sure to give a number from 0 to 100 ;)");
        }

        public void DisplayCustomDiscountMessage() {
            Console.WriteLine("Do you wish to apply an additional discount on another product.\n" +
                "Enter y for 'yes' or anything else for 'no'");
        }

        public void DisplayProductNotFoundMessage() {
            Console.WriteLine("Product was not found.");
        }

        public void DisplayDemandProductUpcMessage()
        {
            Console.WriteLine("Please enter the UPC for a Product you would like to hava an additional discount on or enter -1 to exit loop.");
        }

        public void DisplayOrderOfCalculationsMessage()
        {
            Console.WriteLine("Please enter 1 for the tax to be calculated first, or 2 for discount to be calculated first");
        }

        public void DisplayAddExpensesMessage()
        {
            Console.WriteLine("Would you like to add expenses to a specific product, enter 'y' any other key for no." );
        }

        public void DisplayExpenseAmountMessage()
        {
            Console.WriteLine("Please enter the additional costs");
        }

        public void DisplayExpenseDescriptionMessage()
        {
            Console.WriteLine("What is this Expense? ex: packaging, shipping, etc.");
        }

        public void DisplayIsPricePercentageMessage()
        {
            Console.WriteLine("Is this a percentage of the product price, enter 'y' for yes or any other key for no.");
        }

        public void DisplayExpensePercentageMessage()
        {
            Console.WriteLine("Please enter a real number from 0 to 100 for the added expense rate");
        }

        public void DisplayWantsToExitMessage()
        {
            Console.WriteLine("Enter -1 if you want to exit the loop");
        }

        public void DisplayDemandUpcForExpenseMessage()
        {
            Console.WriteLine("Please enter the UPC for a Product you would like to hava an expense on or enter -1 to exit loop.");
        }

        public void DisplaySumOrMultiplicativeDiscountMessage()
        {
            Console.WriteLine("Would you like the discounts to be simple sum\nEnter 'y' for yes or any other key for no for multiplicative discounts.");
        }

        public void DisplayDemandsDiscountCap()
        {
            Console.WriteLine("Would you like to enter discount caps?\nenter 'y' for yes and anything else as no");
        }

        public void DisplayDemandUpcForDiscountCap()
        {
            Console.WriteLine("Please enter the UPC for a Product you would like to hava a discount cap on or enter -1 to exit loop.");
        }

        public void DisplayDemandDiscountCapPercentage()
        {
            Console.WriteLine("Please enter a real number from 0 to 100 for the added discount cap rate");
        }

        public void DisplayDemandDiscountCapAmount()
        {
            Console.WriteLine("Please enter the maximum discount amount");
        }
    }
}
