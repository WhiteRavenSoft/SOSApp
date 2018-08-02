using System;
using System.Collections.Generic;

namespace SOSApp.Data.AppModel
{
    public class NewsSentModel
    {
        public long ID { get; set; }
        public int NewsID { get; set; }
        public int UserGroupId { get; set; }
        public DateTime SentDate { get; set; }
        public NewsModel News { get; set; }
        public UserGroupModel UserGroup { get; set; }
    }

    public class NewsSentGridModel
    {
        public long ID { get; set; }
        public string NewsTitle { get; set; }
        public string UserGroupName { get; set; }
        public DateTime SentDate { get; set; }
    }

    public class NewsSentPostModel
    {
        public int NewsId { get; set; }
        public List<int> Regions { get; set; }
    }
}
