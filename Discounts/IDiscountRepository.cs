namespace Katana_Tax_Calculator
{
    public interface IDiscountRepository
    {
        List<IDiscount> GetAll();
        IDiscount? GetDiscountByUpc(int upc);
    }
}