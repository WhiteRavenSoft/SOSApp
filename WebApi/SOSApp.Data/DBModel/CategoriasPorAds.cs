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
    
    public partial class CategoriasPorAds
    {
        public long ID { get; set; }
        public long ID_Ads { get; set; }
        public int ID_Categoria { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public System.DateTime TS { get; set; }
    
        public virtual Ads Ads { get; set; }
        public virtual AdsCategoria AdsCategoria { get; set; }
    }
}
