using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Common.Types
{
    [Serializable]
    [Table("Message", Schema = "COR")]
    public class MessageContract: ContractBase
    {
        [Key]
        public int MessageId { get; set; }
        public long CompanyId { get; set; }

        [Required]
        [StringLength(50)]
        public string MessageCode { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        [StringLength(50)]
        public string ClassName { get; set; }

        [StringLength(50)]
        public string PropertyName { get; set; }

        [Required]
        [StringLength(1000)]
        public string MessageText { get; set; }

        public byte? LanguageId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual CompanyContract Company { get; set; }

    }
}
