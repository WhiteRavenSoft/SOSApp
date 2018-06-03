using System;

namespace WhiteRaven.Data.AppModel
{
    public class Beneficio_DetalleModel
    {
        public long ID { get; set; }
        public long ID_Beneficio { get; set; }
        public long ID_Vendedor { get; set; }
        public decimal Monto { get; set; }
        public string Nota { get; set; }
        public DateTime Fecha { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
