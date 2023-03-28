namespace Price_Calculator_Kata
{
    public interface IProductRepository
    {
        IProduct? GetProductByUPC(int UPC);
        List<IProduct> GetAll();
        bool DoesProductExist(int upc);
    }
}