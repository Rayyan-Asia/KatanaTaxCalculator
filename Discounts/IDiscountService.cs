namespace Katana_Tax_Calculator
{
    public interface IDiscountService
    {
        List<IDiscount> GetAll();
        IDiscount? GetDiscountByUpc(int upc);
    }
}