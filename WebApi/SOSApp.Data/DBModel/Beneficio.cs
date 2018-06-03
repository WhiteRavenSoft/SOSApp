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
    
    public partial class Beneficio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Beneficio()
        {
            this.Beneficio_Detalle = new HashSet<Beneficio_Detalle>();
        }
    
        public long ID { get; set; }
        public long ID_InstitucionSocial { get; set; }
        public string Descripcion { get; set; }
        public string Periodo { get; set; }
        public decimal Monto { get; set; }
        public System.DateTime FechaDeseada { get; set; }
        public Nullable<System.DateTime> FechaLograda { get; set; }
        public Nullable<System.DateTime> FechaPago { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public System.DateTime TS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Beneficio_Detalle> Beneficio_Detalle { get; set; }
        public virtual InstitucionSocial InstitucionSocial { get; set; }
    }
}
