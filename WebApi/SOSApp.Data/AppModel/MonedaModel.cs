namespace WhiteRaven.Data.AppModel
{
    public class MonedaModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Formato { get; set; }
        public int Orden { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
