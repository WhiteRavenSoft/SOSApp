namespace WhiteRaven.Data.AppModel
{
    public class AccionUsuarioToRolModel
    {
        public int ID { get; set; }
        public int ID_AccionUsuario { get; set; }
        public int ID_RolUsuario { get; set; }
        public bool SoloLectura { get; set; }
        public bool LecturaYEscritura { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
