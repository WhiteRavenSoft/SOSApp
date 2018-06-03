using System;

namespace WhiteRaven.Data.AppModel
{
    public class EmpresaModel
    {
        public long ID { get; set; }
        public int ID_Rubro { get; set; }
        public int ID_Plan { get; set; }
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
        public string W3w { get; set; }
        public string GoogleMaps { get; set; }
        public string Web { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool MostrarCalificaciones { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }

    public class EmpresaLoginModel {
        public long ID { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
    }
}
