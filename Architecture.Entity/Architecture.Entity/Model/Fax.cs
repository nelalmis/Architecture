//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Architecture.Entity.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fax
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fax()
        {
            this.CompanyContact = new HashSet<CompanyContact>();
            this.PotentialContact = new HashSet<PotentialContact>();
        }
    
        public long FaxId { get; set; }
        public string Fax1 { get; set; }
        public string Fax2 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyContact> CompanyContact { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PotentialContact> PotentialContact { get; set; }
    }
}
