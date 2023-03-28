using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katana_Tax_Calculator
{
    public class CapService : ICapService
    {
        private readonly ICapRepository _repository;
        public CapService(ICapRepository repository)
        {
            _repository = repository;
        }

        public decimal CalculateCap(decimal Price, int upc)
        {
            var cap = GetCapByUpc(upc);
            if(cap.CalculationType == RelativeCalculationType.Amount)
            {
                return cap.Value;
            }
            else
            {
                return Price * (cap.Value / 100);
            }
        }

        public List<ICap> GetAllCaps()
        {
            return _repository.GetAll();
        }

        public ICap? GetCapByUpc(int upc)
        {
            return _repository.GetCapByUpc(upc);
        }

        public bool ProductHasCap(int upc)
        {
            return _repository.ProductHasCap(upc);
        }
    }
}
