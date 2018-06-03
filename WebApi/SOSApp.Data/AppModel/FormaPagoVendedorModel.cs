namespace WhiteRaven.Data.AppModel
{
    public class FormaPagoVendedorModel
    {
        public long ID { get; set; }
        public int ID_FormaPago { get; set; }
        public long ID_Vendedor { get; set; }
        public int Prioridad { get; set; }
        public string Titular { get; set; }
        public string Numero { get; set; }
        public string Documento { get; set; }
        public string Entidad { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
