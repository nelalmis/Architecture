namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COR.Message")]
    public partial class Message
    {
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

        public virtual Company Company { get; set; }
    }
}
