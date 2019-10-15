using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Common.Types
{
    [Serializable]
    [Table("Company", Schema = "COR")]
    public partial class CompanyContract :ContractBase
    {
        public CompanyContract()
        {
            DepartmentList = new HashSet<DepartmentContract>();
            MessageList = new HashSet<MessageContract>();
            ParameterList = new HashSet<ParameterContract>();
            PotentialList = new HashSet<PotentialContract>();
            ResourceList = new HashSet<ResourceContract>();
        }

        [Key]
        public long CompanyId { get; set; }

        [Required]
        [StringLength(500)]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyFullName { get; set; }

        [Required]
        [StringLength(500)]
        public string CompanyIcon { get; set; }

        public byte State { get; set; }

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
        public virtual CompanyContactContract Contact { get; set; }        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual CompanyAuthenticationContract Authentication { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepartmentContract> DepartmentList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MessageContract> MessageList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParameterContract> ParameterList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PotentialContract> PotentialList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResourceContract> ResourceList { get; set; }
    }
}
