using System;

namespace WhiteRaven.Data.AppModel
{
    public class EmpresaHorariosModel
    {
        public long ID { get; set; }
        public long ID_Empresa { get; set; }
        public short Dia { get; set; }
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
