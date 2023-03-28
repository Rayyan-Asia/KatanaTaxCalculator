using Katana_Tax_Calculator;
using Price_Calculator_Kata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KatanaTaxCalculator
{
    public class Calculator : ICalculator
    {
        private readonly decimal DiscountRate = .10m;
        private readonly decimal TaxRate = .20m;
        private readonly int Precision = 2;
        private readonly DiscountCombinationType type = DiscountCombinationType.Multiplicative;
        private readonly IDiscountService _discountService;
        private readonly ICapService _capService;
        private readonly IProductService _productService;
        private readonly IExpenseService _expenseService;

        public Calculator(IDiscountService discountService, ICapService capService, IProductService productService, IExpenseService expenseService)
        {
            _discountService = discountService;
            _capService = capService;
            _productService = productService;
            _expenseService = expenseService;
        }

        public decimal CalculateTaxAmount(IProduct product)
        {

            return ((product.Price - _discountService.CalculateDiscountPrecedence(product.UPC, product.Price, type))
            * TaxRate).Round(Precision);

        }

        public decimal CalculateUniversalDiscount(decimal price)
        {
            return (price * DiscountRate).Round(Precision);
        }

        public decimal CalculateTotalDiscountAmount(IProduct product)
        {
            decimal productDiscount = _discountService.CalculateTotalProductDiscount(product.UPC, product.Price, type).Round(Precision);
            decimal totalDiscount;
            if (type == DiscountCombinationType.Multiplicative)
            {
                totalDiscount = productDiscount + ((product.Price - productDiscount) * DiscountRate);
            }
            else
            {
                totalDiscount = productDiscount + (product.Price * DiscountRate);
            }
            if (_capService.ProductHasCap(product.UPC))
            {
                decimal capAmount = _capService.CalculateCap(product.Price, product.UPC).Round(Precision)
                    .Round(Precision);
                if (totalDiscount > capAmount)
                    return capAmount;
            }
            return totalDiscount.Round(Precision);
        }
        public ICalculationResults CalculateAndSaveResults(int upc)
        {
            var product = _productService.GetProductByUpc(upc);
            List<IExpense> calculatedExpenses = new List<IExpense>();
            var expenseSum = _expenseService.CalculateExpensesAndExport(upc, product.Price, calculatedExpenses).Round(Precision)
                .Round(Precision);
            return CreateResults(product, calculatedExpenses, expenseSum);
        }
        private ICalculationResults CreateResults(IProduct? product, List<IExpense> calculatedExpenses, decimal expenseSum)
        {
            ICalculationResults results = Factory.CreateCalculationResults();
            results.BasePrice = product.Price.Round(Precision);
            results.Name = product.Name;
            calculatedExpenses.ForEach(s => s.Amount = s.Amount.Round(Precision));
            results.CalculatedExpenses = calculatedExpenses;
            results.DiscountAmount = CalculateTotalDiscountAmount(product);
            results.TaxAmount = CalculateTaxAmount(product);
            results.FinalPrice = (product.Price + results.TaxAmount - results.DiscountAmount + expenseSum).Round(Precision);
            return results;
        }



    }
}
