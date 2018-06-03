namespace WhiteRaven.Data.AppModel.External.MercadoPago
{
    public enum SearchStatus
    {
        Paid,
        NotPaid,
        Pending,
        Empty
    }

    public class PaymentSearchResponse
    {
        public int UserID { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public SearchStatus SearchStatus { get; set; }
        public string Id { get; set; }
        public double Total { get; set; }
    }
}
