namespace WhiteRaven.Data.AppModel
{
    public class BancoModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
    }
}
