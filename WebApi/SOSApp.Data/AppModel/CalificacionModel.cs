namespace WhiteRaven.Data.AppModel
{
    public class CalificacionModel
    {
        public long ID { get; set; }
        public long ID_Empresa { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public decimal CalificacionNumero { get; set; }
        public string Comentario { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
