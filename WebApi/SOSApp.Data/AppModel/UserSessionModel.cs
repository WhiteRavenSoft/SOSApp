using System;

namespace WhiteRaven.Data.AppModel
{
    public class UserSessionModel
    {
        public Guid UserSessionGUID { get; set; }
        public int ID_Usuario { get; set; }
        public DateTime UltimoAccesso { get; set; }
        public int? UltimaTrx { get; set; }
    }
}
