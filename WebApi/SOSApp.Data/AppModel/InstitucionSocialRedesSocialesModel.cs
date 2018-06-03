using System;

namespace WhiteRaven.Data.AppModel
{
    public class InstitucionSocialRedesSocialesModel
    {
        public long ID { get; set; }
        public long ID_InstitucionSocial { get; set; }
        public int ID_RedSocial { get; set; }
        public string SocialUserName { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime TS { get; set; }
    }
}
