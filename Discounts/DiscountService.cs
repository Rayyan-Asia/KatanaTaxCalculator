using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katana_Tax_Calculator
{
    public class DiscountService
    {
        private readonly IDiscountRepository _repository;

        public DiscountService(IDiscountRepository repository)
        {
            _repository = repository;
        }

        public List<IDiscount> GetAll()
        {
            return _repository.GetAll();
        }
        public IDiscount? GetDiscountByUpc(int upc)
        {
            return _repository.GetDiscountByUpc(upc);
        }
    }
}
