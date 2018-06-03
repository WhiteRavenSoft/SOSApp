namespace WhiteRaven.Data.AppModel.External.MercadoPago
{
    internal class PaymentNotificationResponse
    {
        public Collection collection { get; set; }

        internal class Collection
        {
            public uint id { get; set; }
            public string site_id { get; set; }
            public string operation_type { get; set; }
            public string order_id { get; set; }
            public string external_reference { get; set; }
            public string status { get; set; }
            public string status_detail { get; set; }
            public string payment_ticket { get; set; }
            public string date_created { get; set; }
            public string last_modified { get; set; }
            public string date_approved { get; set; }
            public string money_release_date { get; set; }
            public string currency_id { get; set; }
            public double transaction_amount { get; set; }
            public double shipping_cost { get; set; }
            public double finance_charge { get; set; }
            public double total_paid_amount { get; set; }
            public double net_received_amount { get; set; }
            public string reason { get; set; }
            public Payer payer { get; set; }
            public Collector collector { get; set; }

            internal class Payer
            {
                public uint id { get; set; }
                public string first_name { get; set; }
                public string last_name { get; set; }
                public string email { get; set; }
                public string nickname { get; set; }
                public Phone phone { get; set; }

                internal class Phone
                {
                    public string area_code { get; set; }
                    public string number { get; set; }
                    public string extension { get; set; }
                }
            }

            internal class Collector
            {
                public uint id { get; set; }
                public string first_name { get; set; }
                public string last_name { get; set; }
                public string email { get; set; }
                public string nickname { get; set; }
                public Phone phone { get; set; }

                internal class Phone
                {
                    public string area_code { get; set; }
                    public string number { get; set; }
                    public string extension { get; set; }
                }
            }
        }
    }
}
