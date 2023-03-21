using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatanaTaxCalculator.KatanaTaxCalculator
{
    public interface IConsoleMessenger
    {
        public void DemandDefaultTaxRateMessage();
        public void DemandTaxRateMessage();
        public void DemandDiscountRateMessage();
        public void SumOrMultiplicativeDiscountMessage();
        public void ErrorMessage(string input);
        public void ExitMessage();
        public void DemandsDiscountCapMessage();
        public void DemandPrecisionMeasurementMessage();
        public void DemandCurrencyMessage();
    }
}
