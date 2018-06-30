using System;

namespace SOSApp.Data.AppModel
{
    public class NewsModel
    {
        public int ID { get; set; }
        public DateTime? Date { get; set; }
        public string Title { get; set; }
        public string Important { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }

    public class NewsGridModel
    {
        public int ID { get; set; }
        public DateTime? Date { get; set; }
        public string Title { get; set; }
        public string Important { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }
    }
}
