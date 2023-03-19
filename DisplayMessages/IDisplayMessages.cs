namespace Price_Calculator_Kata
{
    public interface IDisplayMessages
    { 
        void DisplayErrorMessage(string input);
        void DisplayExitMessage();
        void DisplayWelcomeMessage();
        void DisplayDiscountMessage();
        void DisplayCustomDiscountMessage();
        void DisplayProductNotFoundMessage();
        void DisplayDemandProductUpcMessage();

        void DisplayOrderOfCalculationsMessage();

        void DisplayAddExpensesMessage();
        void DisplayExpenseAmountMessage();
        void DisplayExpenseDescriptionMessage();

        void DisplayIsPricePercentageMessage();
        void DisplayExpensePercentageMessage();

        void DisplayWantsToExitMessage();
        void DisplayDemandUpcForExpenseMessage();

        void DisplaySumOrMultiplicativeDiscountMessage();

        void DisplayDemandsDiscountCap();

        void DisplayDemandUpcForDiscountCap();

        void DisplayDemandDiscountCapPercentage();
        void DisplayDemandDiscountCapAmount();
        void DisplayDemandPrecisionMeasurement();
    }
}