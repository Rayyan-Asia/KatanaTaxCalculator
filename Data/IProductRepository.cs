namespace Price_Calculator_Kata
{
    public interface IProductRepository
    {
        IProduct GetProductByUPC(int UPC);
        List<IProduct> ListProducts();
    }
}