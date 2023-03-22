using Katana_Tax_Calculator;
using Price_Calculator_Kata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatanaTaxCalculator
{
    public class Calculator : ICalculator
    {
        public decimal TaxRate { get; set; }
        public decimal DiscountRate { get; set; }
        private readonly IDiscountService _discountService;
        public DiscountCombinationType type { get; set; }

        public int _precision { get; set; }

        public Calculator(decimal taxRate, decimal discountRate, IDiscountService discountService, DiscountCombinationType type, int precision)
        {
            TaxRate = taxRate;
            DiscountRate = discountRate;
            this._discountService = discountService;
            this.type = type;
            _precision = precision;
        }

        public decimal CalculateTaxAmount(IProduct product)
        {
            if (IsDiscountBefore(product))
            {
                return ((product.Price - CalculateProductDiscount(product)) * TaxRate).Round(_precision);
            }
            else
            {
                return (product.Price * TaxRate).Round(_precision);
            }
        }

        public decimal CalculateUniversalDiscount(decimal price)
        {
            return (price * DiscountRate).Round(_precision);
        }

        public decimal CalculateProductDiscount(IProduct product)
        {
            var discount = _discountService.GetDiscountByUpc(product.UPC);
            if (discount != null)
            {
                var discountPercentage = discount.Percentage;
                if (discountPercentage != 0)
                    return (product.Price * discountPercentage).Round(_precision);
            }
            return 0;
        }
        public bool IsDiscountBefore(IProduct product)
        {
            var discount = _discountService.GetDiscountByUpc(product.UPC);
            if (discount != null && discount.type == DiscountOrderType.BeforeTax)
                return true;
            return false;
        }

        public decimal CalculteSimpleSumDiscount(IProduct product)
        {
            return (CalculateProductDiscount(product) + CalculateUniversalDiscount(product.Price)).Round(_precision);
        }

        public decimal CalculateMultiplicativeDiscount(IProduct product)
        {
            decimal firstDiscount = CalculateProductDiscount(product);
            return CalculateUniversalDiscount(product.Price - firstDiscount);
        }

        public decimal CalculateTotalDiscountAmount(IProduct product, Cap? cap)
        {
            decimal totalDiscount;
            if (type == DiscountCombinationType.Additive)
                totalDiscount = CalculteSimpleSumDiscount(product);
            else
                totalDiscount = CalculateMultiplicativeDiscount(product);

            if (cap != null)
            {
                decimal capAmount = CalculateCap(product, cap);
                if (totalDiscount > capAmount)
                    return capAmount;
            }
            return totalDiscount;
        }

        public decimal CalculateCap(IProduct product, Cap cap)
        {
            if (cap.CalculationType == RelativeCalculationType.Amount)
                return cap.Value.Round(_precision);
            return (cap.Value * product.Price).Round(_precision);
        }

        public decimal CalculateExpense(IProduct product, IExpense expense)
        {
            return (product.Price * expense.Amount).Round(_precision);
        }
        public decimal CalculateTotalPrice(IProduct product, decimal ExpenseSum, Cap? cap)
        {
            return (product.Price + CalculateTaxAmount(product) - CalculateTotalDiscountAmount(product, cap) + ExpenseSum.Round(_precision)).Round(_precision);
        }

    }
}
