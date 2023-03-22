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

        public List<ICap> GetAllCaps()
        {
            return _repository.GetAll();
        }

        public ICap? GetCapByUpc(int upc)
        {
            return _repository.GetCapByUpc(upc);
        }
    }
}
