namespace Katana_Tax_Calculator
{
    public interface ICapRepository
    {
        List<ICap> GetAll();
        ICap? GetCapByUpc(int upc);
        bool ProductHasCap(int upc);
    }
}