namespace Katana_Tax_Calculator
{
    public interface ICapService
    {
        List<ICap> GetAllCaps();
        ICap? GetCapByUpc(int upc);
    }
}