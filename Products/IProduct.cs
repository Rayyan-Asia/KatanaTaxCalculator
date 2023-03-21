namespace Price_Calculator_Kata
{
    public interface IProduct
    {
        public string Name { get; set; }
        public int UPC { get; set; }
        public double Price { get; set; }
        
    }
}