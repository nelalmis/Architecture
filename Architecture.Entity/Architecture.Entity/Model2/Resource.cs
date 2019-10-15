namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COR.Resource")]
    public partial class Resource
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Resource()
        {
            ResourceAction = new HashSet<ResourceAction>();
        }

        public int ResourceId { get; set; }

        public long CompanyId { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Text { get; set; }

        [StringLength(250)]
        public string AssemblyName { get; set; }

        [StringLength(100)]
        public string ControllerName { get; set; }

        [StringLength(100)]
        public string ViewName { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        public int? ModuleId { get; set; }

        public int? ParentId { get; set; }

        public byte? MenuType { get; set; }

        public byte? SortId { get; set; }

        public byte? ViewType { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(10)]
        public string UserName { get; set; }

        [StringLength(20)]
        public string HostName { get; set; }

        public DateTime? SystemDate { get; set; }

        [StringLength(10)]
        public string UpdateUserName { get; set; }

        public DateTime? UpdateSystemDate { get; set; }

        [StringLength(28)]
        public string OperationKey { get; set; }

        public virtual Company Company { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResourceAction> ResourceAction { get; set; }
    }
}
