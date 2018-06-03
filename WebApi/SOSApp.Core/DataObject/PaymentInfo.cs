using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteRaven.Core.DataObject
{
    [Serializable]
    public class PaymentInfo
    {
        public string CreditCardType { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string CardCode { get; set; }
        public decimal Amount { get; set; }
        public string Detail { get; set; }
    }
}
