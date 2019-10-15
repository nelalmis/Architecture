using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Common.Types
{
    [Serializable]
    [Table("ResourceAction", Schema = "COR")]
    public partial class ResourceActionContract :ContractBase
    {
        [Key]
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

        //TabloDışı
        public string ActionTypeName { get; set; }

        public bool IsExists { get; set; }

        [ForeignKey("ResourceId")]
        public virtual ResourceContract Resource { get; set; }

        [ForeignKey("ActionId")]
        public virtual ActionContract Action { get; set; }
    }
}
