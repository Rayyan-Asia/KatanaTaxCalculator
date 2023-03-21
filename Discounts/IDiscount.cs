namespace Katana_Tax_Calculator
{
    public interface IDiscount
    {
        decimal Percentage { get; set; }
        DiscountOrderType type { get; set; }
        int UPC { get; set; }
    }
}