namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COR.ResourceAction")]
    public partial class ResourceAction
    {
        public int ResourceActionId { get; set; }

        public int ResourceId { get; set; }

        public int? ActionId { get; set; }

        [Required]
        [StringLength(50)]
        public string CommandName { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayName { get; set; }

        [StringLength(100)]
        public string Icon { get; set; }

        public byte? ActionType { get; set; }

        public byte? SortId { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public virtual Action Action { get; set; }

        public virtual Resource Resource { get; set; }
    }
}
