namespace SOSApp.Data.AppModel
{
    public class GridFilter
    {
        public string Property { get; set; }
        public object Value { get; set; }
    }

    public class GridSort
    {
        public string Property { get; set; }
        public string Direction { get; set; }
    }
}
