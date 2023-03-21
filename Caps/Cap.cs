using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katana_Tax_Calculator
{
    public class Cap : ICap
    {
        public decimal Value { get; set; }
        public RelativeCalculationType CalculationType { get; set; }
        public int UPC { get; set; }
        public Cap(decimal value, RelativeCalculationType calculationType, int uPC)
        {
            Value = value;
            CalculationType = calculationType;
            UPC = uPC;
        }
    }
}
