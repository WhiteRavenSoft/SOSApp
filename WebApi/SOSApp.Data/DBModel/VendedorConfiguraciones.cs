//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WhiteRaven.Data.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class VendedorConfiguraciones
    {
        public long ID { get; set; }
        public long ID_Vendedor { get; set; }
        public string StringKey { get; set; }
        public string StringValue { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Vendedor Vendedor { get; set; }
    }
}
