namespace SOSApp.Data.AppModel
{
    public class AppResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
