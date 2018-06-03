using System;

namespace WhiteRaven.Data.AppModel
{
    public class PagoModel
    {
        public int ID { get; set; }
        public int? ID_Usuario { get; set; }
        public decimal? Monto { get; set; }
        public string CodigoTrx { get; set; }
        public string TraceTrx { get; set; }
        public DateTime? FechaCrecion { get; set; }
        public int? Proveedor { get; set; }
        public int? Estado { get; set; }
        public int? Tipo { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
