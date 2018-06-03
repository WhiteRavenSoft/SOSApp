namespace WhiteRaven.Data.AppModel
{
    public class CiudadModel
    {
        public long ID { get; set; }
        public int ID_Provincia { get; set; }
        public ProvinciaModel Provincia { get; set; }
        public string Nombre { get; set; }
        public string CodigoPostal { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
