namespace WhiteRaven.Data.AppModel
{
    public class VendedorModel
    {
        public long ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string CUIT { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public string Avatar { get; set; }
        public string Direccion { get; set; }
        public long ID_Ciudad { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
