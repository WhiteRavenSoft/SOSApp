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
    
    public partial class Vendedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vendedor()
        {
            this.FormaPagoVendedor = new HashSet<FormaPagoVendedor>();
            this.VendedorConfiguraciones = new HashSet<VendedorConfiguraciones>();
            this.VendedorRedesSociales = new HashSet<VendedorRedesSociales>();
            this.Ventas = new HashSet<Ventas>();
            this.VendedorCuenta = new HashSet<VendedorCuenta>();
        }
    
        public long ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string CUIT { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Telefono { get; set; }
        public string Avatar { get; set; }
        public string Direccion { get; set; }
        public long ID_Ciudad { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public System.DateTime TS { get; set; }
    
        public virtual Ciudad Ciudad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormaPagoVendedor> FormaPagoVendedor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendedorConfiguraciones> VendedorConfiguraciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendedorRedesSociales> VendedorRedesSociales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ventas> Ventas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendedorCuenta> VendedorCuenta { get; set; }
    }
}
