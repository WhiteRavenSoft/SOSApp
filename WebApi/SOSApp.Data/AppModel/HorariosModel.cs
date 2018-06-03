using System;

namespace WhiteRaven.Data.AppModel
{
    public class HorariosModel
    {
        public long ID { get; set; }
        public long ID_Empresa { get; set; }
        public short Dia { get; set; }
        public TimeSpan Abre { get; set; }
        public TimeSpan Cierra { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
