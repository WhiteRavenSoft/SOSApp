using System;

namespace WhiteRaven.Data.AppModel
{
    public class SysdiagramsModel
    {
        public string Name { get; set; }
        public int Principal_id { get; set; }
        public int Diagram_id { get; set; }
        public int? Version { get; set; }
        public byte[] Definition { get; set; }
    }
}
