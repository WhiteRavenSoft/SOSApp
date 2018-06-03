namespace WhiteRaven.Data.AppModel
{
    public class W3wResponse
    {
        public W3wGeometry geometry { get; set; }
        public string words { get; set; }
        public W3wStatus status { get; set; }
    }

    public class W3wStatus
    {
        public string code { get; set; }
        public string message { get; set; }
        public string reason { get; set; }
        public string status { get; set; }
    }

    public class W3wGeometry
    {
        public string lng { get; set; }
        public string lat { get; set; }

    }
}
