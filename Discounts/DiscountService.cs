using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katana_Tax_Calculator
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _repository;

        public DiscountService(IDiscountRepository repository)
        {
            _repository = repository;
        }

        public decimal CalculateDiscountPrecedence(int upc, decimal price, DiscountCombinationType type)
        {
           var discounts =  GetDiscountsByUpc(upc).Where(x => x.type == DiscountOrderType.BeforeTax);
            var totalDiscount = 0.0m;
            
            if(type == DiscountCombinationType.Multiplicative)
            {
                decimal alteredPrice = price;
                foreach (var discount in discounts)
                {
                    var amount = discount.Percentage * alteredPrice;
                    totalDiscount += amount;
                    alteredPrice -= amount;  
                }
                return totalDiscount;
            }
            else
            {
                return discounts.Sum(x => x.Percentage * price);
            }
            
        }

        public decimal CalculateTotalProductDiscount(int upc, decimal price, DiscountCombinationType type)
        {
            var discounts = GetDiscountsByUpc(upc);
            var totalDiscount = 0.0m;

            if (type == DiscountCombinationType.Multiplicative)
            {
                decimal alteredPrice = price;
                foreach (var discount in discounts)
                {
                    var amount = discount.Percentage * alteredPrice;
                    totalDiscount += amount;
                    alteredPrice -= amount;
                }
                return totalDiscount;
            }
            else
            {
                return discounts.Sum(x => x.Percentage * price);
            }
        }

        public List<IDiscount> GetAll()
        {
            return _repository.GetAll();
        }
        public List<IDiscount> GetDiscountsByUpc(int upc)
        {
            return _repository.GetDiscountsByUpc(upc);
        }
    }
}
