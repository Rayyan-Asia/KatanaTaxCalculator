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
                    Price = 21.50M,
                },
                new Product()
                {
                    Name = "PS5",
                    UPC = 654321,
                    Price = 21.50M,
                },
            };
        }
        public List<IProduct> GetAll()
        {
            return _products;
        }

        public IProduct? GetProductByUPC(int upc)
        {
            return _products.SingleOrDefault(s => s.UPC == upc);
        }
    }
}
