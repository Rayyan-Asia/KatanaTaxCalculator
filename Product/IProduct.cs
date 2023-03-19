namespace Price_Calculator_Kata
{
    public interface IProduct
    {
        public string Name { get; set; }
        public int UPC { get; set; }
        public double Price { get; set; }
        public decimal Discount { get; set; }
        public double DiscountCap { get; set; }
        public List<IExpense> Expenses { get; set; }
    }
}