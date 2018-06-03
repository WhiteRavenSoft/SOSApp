using System.Collections.Generic;

namespace WhiteRaven.Data.AppModel.External.MercadoPago
{
    public class PaymentPreferenceRequest
    {
        public List<Items> items { get; set; }
        public string external_reference { get; set; }
        public Payer payer { get; set; }
        public BackUrls back_urls { get; set; }
        public PaymentMethods payment_methods { get; set; }
        public string auto_return { get; set; }

        public class Items
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public uint quantity { get; set; }
            public double unit_price { get; set; }
            public string currency_id { get; set; }
            public string picture_url { get; set; }
        }

        public class Payer
        {
            //public string name { get; set; }
            //public string surname { get; set; }
            public string email { get; set; }
        }

        public class BackUrls
        {
            public string success { get; set; }
            public string failure { get; set; }
            public string pending { get; set; }
        }

        public class PaymentMethods
        {
            public List<ExcludedPaymentMethods> excluded_payment_methods { get; set; }
            public List<ExcludedPaymentTypes> excluded_payment_types { get; set; }
            public uint? installments { get; set; } //debe ser nullable mp no acepta installments = 0

            public class ExcludedPaymentMethods
            {
                public string id { get; set; }
            }

            public class ExcludedPaymentTypes
            {
                public string id { get; set; }
            }
        }
    }
}
