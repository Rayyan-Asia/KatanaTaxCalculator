using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Price_Calculator_Kata;

namespace Katana_Tax_Calculator
{
    public class ReportGenerator : IReportGenerator
    {
        public IDisplayMessages messenger { get; set; }
        public ILogger logger { get; set; }
        public IProductPriceCalculator priceCalculator { get; set; }
        public IProductRepository repository { get; set; }
        public ReportGenerator(IDisplayMessages messenger, ILogger logger, IProductRepository repository, IProductPriceCalculator priceCalculator)
        {
            this.repository = repository;
            this.messenger = messenger;
            this.logger = logger;
            this.priceCalculator = priceCalculator;
        }

        public void GenerateReport(decimal taxRate, decimal discountRate, bool discountfirst, int precision)
        {
            var products = repository.ListProducts();
            IterateAndReport(taxRate, discountRate, discountfirst, precision, products);

        }

        public void IterateAndReport(decimal taxRate, decimal discountRate, bool discountfirst, int precision, List<IProduct> products)
        {
            foreach (IProduct product in products)
            {
                if (discountfirst)
                {
                    ReportDiscountFirst(taxRate, discountRate, product, precision);
                }
                else
                {
                    messenger.DisplaySumOrMultiplicativeDiscountMessage();
                    var input = Console.ReadLine().Replace(" ", "").ToLower();
                    var priceAfterTax = priceCalculator.CalculatePriceWithGivenTaxRate(product, taxRate, precision);
                    var priceAfterDiscount = 0.0;
                    bool multiplicative = false;
                    if (input == "y")
                    {
                        priceAfterDiscount = priceCalculator.CalculatePriceWithGivenDiscountRateAfterTax(product, priceAfterTax, discountRate, precision);
                    }
                    else
                    {
                        priceAfterDiscount = priceCalculator.CalculatePriceWithMultiplicativeDiscount(product, priceAfterTax, discountRate, precision);
                        multiplicative = true;
                    }
                    if (product.DiscountCap != 0 && priceAfterTax - priceAfterDiscount > product.DiscountCap)
                    {
                        priceAfterDiscount = priceAfterTax - product.DiscountCap;
                        logger.PrintProduct(product, precision);
                        logger.PrintTax(product.Price, priceAfterTax, taxRate, product.Currency, precision);
                        logger.PrintCapDeduction(priceAfterTax, priceAfterDiscount, product.Currency, precision);
                        double totalExpenses = priceCalculator.CalculateExpenses(priceCalculator, logger, product, precision);
                        logger.PrintFinalPrice(priceAfterDiscount + totalExpenses, product.Currency, precision);
                    }
                    else
                    {
                        ReportDiscountAfter(taxRate, discountRate, product, priceAfterTax, priceAfterDiscount, multiplicative, precision);
                    }
                }

            }
        }

        public void ReportDiscountAfter(decimal taxRate, decimal discountRate, IProduct product, double priceAfterTax, double priceAfterDiscount, bool multiplicative, int precision)
        {
            logger.PrintProduct(product, precision);
            logger.PrintTax(product.Price, priceAfterTax, taxRate, product.Currency, precision);
            if (!multiplicative)
            {
                logger.PrintDiscount(priceAfterTax, priceAfterDiscount, discountRate + product.Discount, product.Currency, precision);
            }
            else
            {
                logger.PrintMultiplicativeDiscount(priceAfterTax, priceAfterDiscount, product.Discount, discountRate, product.Currency, precision);
            }
            double totalExpenses = priceCalculator.CalculateExpenses(priceCalculator, logger, product, precision);
            logger.PrintFinalPrice(priceAfterDiscount + totalExpenses, product.Currency, precision);
        }

        public void ReportDiscountFirst(decimal taxRate, decimal discountRate, IProduct product, int precision)
        {
            var priceAfterDiscount = priceCalculator.CalculatePriceWithGivenDiscountRate(product, precision);
            var cap = product.DiscountCap;
            if (cap == 0)
            {
                ReportWithoutCap(taxRate, discountRate, priceCalculator, logger, product, precision, priceAfterDiscount);
            }
            else
            {
                cap -= product.Price - priceAfterDiscount;
                if (cap < 0)
                {
                    ReportCapReachedAtFirstDiscount(taxRate, product, precision);
                }
                else
                {
                    var priceAfterTax = priceCalculator.CalculatePriceWithGivenTaxRateAfterDiscount(priceAfterDiscount, taxRate, precision);
                    var priceAfterSecondDiscount = priceCalculator.CalculatePriceWithGivenDiscountRateAfterTaxWithoutUPC(product, priceAfterTax, discountRate, precision);
                    if (cap < priceAfterTax - priceAfterSecondDiscount)
                    {
                        ReportCapReachedAtSecondDiscount(taxRate, discountRate, product, precision, priceAfterDiscount, cap, priceAfterTax);
                    }
                    else
                    {
                        ReportProduct(taxRate, discountRate, product, precision, priceAfterDiscount, priceAfterTax, priceAfterSecondDiscount);
                    }
                }
            }
        }

        public void ReportCapReachedAtSecondDiscount(decimal taxRate, decimal discountRate, IProduct product, int precision, double priceAfterDiscount, double cap, double priceAfterTax)
        {
            double priceAfterSecondDiscount = priceAfterTax - cap;
            logger.PrintProduct(product, precision);
            logger.PrintDiscount(product.Price, priceAfterDiscount, discountRate, product.Currency, precision);
            logger.PrintTax(priceAfterDiscount, priceAfterTax, taxRate, product.Currency, precision);
            logger.PrintCapDeduction(priceAfterTax, priceAfterSecondDiscount, product.Currency, precision);
            double totalExpenses = priceCalculator.CalculateExpenses(priceCalculator, logger, product, precision);
            logger.PrintFinalPrice(priceAfterSecondDiscount + totalExpenses, product.Currency, precision);
        }

        public void ReportCapReachedAtFirstDiscount(decimal taxRate, IProduct product, int precision)
        {
            double priceAfterDiscount = product.Price - product.DiscountCap;
            var priceAfterTax = priceCalculator.CalculatePriceWithGivenTaxRateAfterDiscount(priceAfterDiscount, taxRate, precision);
            logger.PrintProduct(product, precision);
            logger.PrintCapDeduction(product.Price, priceAfterDiscount, product.Currency, precision);
            logger.PrintTax(priceAfterDiscount, priceAfterTax, taxRate, product.Currency, precision);
            double totalExpenses = priceCalculator.CalculateExpenses(priceCalculator, logger, product, precision);
            logger.PrintFinalPrice(priceAfterTax + totalExpenses, product.Currency, precision);
        }

        public void ReportWithoutCap(decimal taxRate, decimal discountRate, IProductPriceCalculator priceCalculator, ILogger logger, IProduct product, int precision, double priceAfterDiscount)
        {
            var priceAfterTax = priceCalculator.CalculatePriceWithGivenTaxRateAfterDiscount(priceAfterDiscount, taxRate, precision);
            var priceAfterSecondDiscount = priceCalculator.CalculatePriceWithGivenDiscountRateAfterTaxWithoutUPC(product, priceAfterTax, discountRate, precision);
            ReportProduct(taxRate, discountRate, product, precision, priceAfterDiscount, priceAfterTax, priceAfterSecondDiscount);
        }

        public void ReportProduct(decimal taxRate, decimal discountRate, IProduct product, int precision, double priceAfterDiscount, double priceAfterTax, double priceAfterSecondDiscount)
        {
            logger.PrintProduct(product, precision);
            if (product.Discount != 0) { logger.PrintDiscount(product.Price, priceAfterDiscount, product.Discount, product.Currency, precision); }
            logger.PrintTax(priceAfterDiscount, priceAfterTax, taxRate, product.Currency, precision);
            logger.PrintDiscount(priceAfterTax, priceAfterSecondDiscount, discountRate, product.Currency, precision);
            double totalExpenses = priceCalculator.CalculateExpenses(priceCalculator, logger, product, precision);
            logger.PrintFinalPrice(priceAfterSecondDiscount + totalExpenses, product.Currency, precision);
        }
    }
}
