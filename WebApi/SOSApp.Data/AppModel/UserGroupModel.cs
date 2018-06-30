using System;

namespace SOSApp.Data.AppModel
{
    public class UserGroupModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
    }
}