namespace Katana_Tax_Calculator
{
    public interface IDiscountRepository
    {
        List<IDiscount> GetAll();
        List<IDiscount> GetDiscountsByUpc(int upc);
    }
}