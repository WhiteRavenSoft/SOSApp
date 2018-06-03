using System;

namespace WhiteRaven.Data.AppModel
{
    public class VentasModel
    {
        public long ID { get; set; }
        public long ID_Empresa { get; set; }
        public long ID_Vendedor { get; set; }
        public string Periodo { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public DateTime? FechaLiquidacion { get; set; }
        public decimal MontoACobrar { get; set; }
        public decimal MontoAPagar { get; set; }
        public decimal MontoSocial { get; set; }
        public bool Cobrado { get; set; }
        public bool Pagado { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
