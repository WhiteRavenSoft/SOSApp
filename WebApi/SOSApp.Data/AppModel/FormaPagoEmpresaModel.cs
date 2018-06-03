namespace WhiteRaven.Data.AppModel
{
    public class FormaPagoEmpresaModel
    {
        public long ID { get; set; }
        public int ID_FormaPago { get; set; }
        public long ID_Empresa { get; set; }
        public int Prioridad { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
