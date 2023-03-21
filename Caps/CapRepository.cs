using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Katana_Tax_Calculator
{
    public class CapRepository : ICapRepository
    {
        private readonly List<ICap> _caps;
        public CapRepository()
        {
            _caps = new List<ICap>()
            {
                new Cap(5,RelativeCalculationType.Amount,123456),
                new Cap(20,RelativeCalculationType.Percent,654321)
            };
        }
        public List<ICap> GetAll()
        {
            return _caps;
        }

        public ICap? GetCapByUpc(int upc)
        {
            return _caps.SingleOrDefault(s => s.UPC == upc);
        }
    }
}
