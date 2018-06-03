namespace WhiteRaven.Data.AppModel
{
    public class AdsCategoriaModel
    {
        public int ID { get; set; }
        public int? ID_CategoriaPadre { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string BackgrundImage { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
