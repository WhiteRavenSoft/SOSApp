namespace WhiteRaven.Data.AppModel
{
    public class EmpresaRedesSocialesModel
    {
        public long ID { get; set; }
        public long ID_Empresa { get; set; }
        public int ID_RedSocial { get; set; }
        public string SocialUserName { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
