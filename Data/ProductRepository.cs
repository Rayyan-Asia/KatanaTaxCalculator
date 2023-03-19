using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_Kata
{
    public class ProductRepository : IProductRepository
    {
        private List<IProduct> _products;
        public ProductRepository()
        {
            _products = new List<IProduct>()
            {
                new Product()
                {
                    Name = "XBox",
                    UPC = 123456,
                    Price = 21.50,
                },
                new Product()
                {
                    Name = "PS5",
                    UPC = 654321,
                    Price = 25.00,
                },
            };
        }
        public List<IProduct> ListProducts()
        {
            return _products;
        }

        public IProduct GetProductByUPC(int UPC)
        {
            return _products.SingleOrDefault(s => s.UPC == UPC);
        }
    }
}
