using System;

namespace WhiteRaven.Data.AppModel
{
    public class EmpresaHorariosEspecialesModel
    {
        public long ID { get; set; }
        public long ID_Empresa { get; set; }
        public DateTime Dia { get; set; }
        public bool Abierto { get; set; }
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
