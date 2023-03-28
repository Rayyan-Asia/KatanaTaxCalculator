using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public interface IProductService
    {
        List<IProduct> GetAll();
        IProduct? GetProductByUpc(int upc);
        bool DoesProductExist(int upc);
    }
}