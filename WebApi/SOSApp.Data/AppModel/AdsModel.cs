namespace WhiteRaven.Data.AppModel
{
    public class AdsModel
    {
        public long ID { get; set; }
        public long ID_Empresa { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public short Largo { get; set; }
        public short Alto { get; set; }
        public string ColorPrincipal { get; set; }
        public string ColorSecundario { get; set; }
        public string ColorTexto { get; set; }
        public string Imagen { get; set; }
        public bool Principal { get; set; }
        public bool Destacado { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
