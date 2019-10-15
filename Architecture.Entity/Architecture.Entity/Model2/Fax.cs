namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CNT.Fax")]
    public partial class Fax
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fax()
        {
            CompanyContact = new HashSet<CompanyContact>();
            PotentialContact = new HashSet<PotentialContact>();
        }

        public long FaxId { get; set; }

        [Column("Fax")]
        [Required]
        [StringLength(24)]
        public string Fax1 { get; set; }

        [StringLength(24)]
        public string Fax2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyContact> CompanyContact { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PotentialContact> PotentialContact { get; set; }
    }
}
