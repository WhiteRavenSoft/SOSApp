using System;

namespace WhiteRaven.Data.AppModel
{
    public class EmpresaCuentaModel
    {
        public int ID { get; set; }
        public long ID_Empresa { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoBloqueado { get; set; }
        public decimal MontoGastado { get; set; }
        public DateTime Fecha { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
