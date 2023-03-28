using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katana_Tax_Calculator
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly List<IDiscount> _discounts;
        public DiscountRepository()
        {
            _discounts = new List<IDiscount>()
            {
                new Discount(.10M,123456,DiscountOrderType.AfterTax),
                new Discount(.10M,654321,DiscountOrderType.BeforeTax)
            };
        }

        public List<IDiscount> GetAll()
        {
            return _discounts;
        }

        public List<IDiscount> GetDiscountsByUpc(int upc)
        {
            return _discounts.Where(s => s.UPC == upc).ToList();
        }
    }
}
