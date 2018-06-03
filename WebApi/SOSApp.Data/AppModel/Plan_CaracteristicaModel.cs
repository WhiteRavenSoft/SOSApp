namespace WhiteRaven.Data.AppModel
{
    public class Plan_CaracteristicaModel
    {
        public int ID { get; set; }
        public int ID_Plan { get; set; }
        public string Caracteristica { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
