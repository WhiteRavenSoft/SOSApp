using System;

namespace WhiteRaven.Data.AppModel
{
    public class UsuarioAccionModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }

    }
}
