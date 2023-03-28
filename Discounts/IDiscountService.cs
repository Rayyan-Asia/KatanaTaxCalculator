namespace Katana_Tax_Calculator
{
    public interface IDiscountService
    {
        List<IDiscount> GetAll();
        List<IDiscount> GetDiscountsByUpc(int upc);
        decimal CalculateDiscountPrecedence(int upc, decimal price, DiscountCombinationType type);
        decimal CalculateTotalProductDiscount(int upc, decimal price, DiscountCombinationType type);
    }
}