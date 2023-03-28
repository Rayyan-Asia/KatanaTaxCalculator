using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            this._repository = repository;
        }

        public bool DoesProductExist(int upc)
        {
            return _repository.DoesProductExist(upc);
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
