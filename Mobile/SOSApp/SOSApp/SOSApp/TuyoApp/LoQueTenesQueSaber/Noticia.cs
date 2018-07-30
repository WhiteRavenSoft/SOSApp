using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SOSApp.Helpers;
using Xamarin.Forms;

namespace SOSApp.TuyoApp.LoQueTenesQueSaber
{

    public class Noticia
    {
        public int Code { get; set; }
        public object Message { get; set; }
        public Detalle[] data { get; set; }
        public int Page { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public object[] Filter { get; set; }
        public object[] Sort { get; set; }
    }

    public class Detalle
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Important { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }

    public class RootDetalle
    {
        public int Code { get; set; }
        public object Message { get; set; }
        public Detalle data { get; set; }
        public int Page { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public object[] Filter { get; set; }
        public object[] Sort { get; set; }
    }
}
