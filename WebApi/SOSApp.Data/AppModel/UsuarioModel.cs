using System;

namespace WhiteRaven.Data.AppModel
{
    public class UsuarioModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int? AuthenticationMode { get; set; }
        public string Pwd { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        public int? ID_RolUsuario { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
