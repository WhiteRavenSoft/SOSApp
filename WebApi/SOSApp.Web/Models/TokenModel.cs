namespace SOSApp.Web.Models
{
    public class TokenModel
    {
        public string access_token { get; set; }
        public string given_name { get; set; }
        public string email { get; set; }
        public string entity_id { get; set; }
        public string expires_in { get; set; }
    }
}