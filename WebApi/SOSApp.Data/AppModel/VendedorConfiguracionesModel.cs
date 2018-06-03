﻿using System;

namespace WhiteRaven.Data.AppModel
{
    public class VendedorConfiguracionesModel
    {
        public long ID { get; set; }
        public long ID_Vendedor { get; set; }
        public string StringKey { get; set; }
        public string StringValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
    }
}
