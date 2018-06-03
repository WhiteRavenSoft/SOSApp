using System;

namespace WhiteRaven.Data.AppModel
{
    public class InstitucionSocialModel
    {
        public long ID { get; set; }
        public string RazonSocial { get; set; }
        public string CUIT { get; set; }
        public string Password { get; set; }
        public string PerfilLogo { get; set; }
        public string PerfilCabecera { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public long ID_Ciudad { get; set; }
        public string Mapa_Latitud { get; set; }
        public string Mapa_Longitud { get; set; }
        public string Web { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
