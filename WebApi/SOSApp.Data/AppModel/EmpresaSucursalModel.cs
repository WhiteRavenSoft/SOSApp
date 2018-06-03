namespace WhiteRaven.Data.AppModel
{
    public class EmpresaSucursalModel
    {
        public long ID { get; set; }
        public long? ID_Empresa { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Nota { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
