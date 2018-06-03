using System;

namespace WhiteRaven.Data.AppModel
{
    public class BeneficioModel
    {
        public long ID { get; set; }
        public long ID_InstitucionSocial { get; set; }
        public string Descripcion { get; set; }
        public string Periodo { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaDeseada { get; set; }
        public DateTime? FechaLograda { get; set; }
        public DateTime? FechaPago { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
