namespace Price_Calculator_Kata
{
    public interface IDisplayMessages
    {
        void DisplayErrorMessage(string input);
        void DisplayExitMessage();
        void DisplayDemandTaxRateMessage();
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
        void DisplayDemandsDiscountCapMessage();
        void DisplayDemandUpcForDiscountCapMessage();
        void DisplayDemandDiscountCapPercentageMessage();
        void DisplayDemandDiscountCapAmountMessage();
        void DisplayDemandPrecisionMeasurementMessage();
        void DisplayDemandDefaultTaxRateMessage();
    }
}