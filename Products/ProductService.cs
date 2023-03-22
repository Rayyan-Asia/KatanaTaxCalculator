using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Price_Calculator_Kata;

namespace Katana_Tax_Calculator.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            this._repository = repository;
        }

        public List<IProduct> GetAll()
        {
            return _repository.GetAll();
        }

        public IProduct? GetProductByUpc(int upc)
        {
            return _repository.GetProductByUPC(upc);
        }
    }
}
