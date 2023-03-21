namespace Katana_Tax_Calculator
{
    public interface ICap
    {
        RelativeCalculationType CalculationType { get; set; }
        int UPC { get; set; }
        decimal Value { get; set; }
    }
}