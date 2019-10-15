namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COR.Potential")]
    public partial class Potential
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Potential()
        {
            PotentialContact = new HashSet<PotentialContact>();
            Authentication = new HashSet<Authentication>();
            CustomerDetail = new HashSet<CustomerDetail>();
            UserDetail = new HashSet<UserDetail>();
        }

        public long PotentialId { get; set; }

        public long CompanyId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(11)]
        public string Ssn { get; set; }

        [StringLength(1)]
        public string Sex { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public byte Status { get; set; }

        public byte PotentialType { get; set; }

        public byte PersonType { get; set; }

        [StringLength(10)]
        public string UserName { get; set; }

        [StringLength(20)]
        public string HostName { get; set; }

        public DateTime? SystemDate { get; set; }

        [StringLength(10)]
        public string UpdateUserName { get; set; }

        public DateTime? UpdateSystemDate { get; set; }

        [StringLength(15)]
        public string HostIp { get; set; }

        [StringLength(28)]
        public string OperationKey { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PotentialContact> PotentialContact { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Authentication> Authentication { get; set; }

        public virtual Company Company { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerDetail> CustomerDetail { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDetail> UserDetail { get; set; }
    }
}
