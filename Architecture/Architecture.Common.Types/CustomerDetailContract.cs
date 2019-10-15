using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Common.Types
{
    [Serializable]
    [Table("CustomerDetail", Schema = "COR")]
    public class CustomerDetailContract
    {
        [Key]
        public int CustomerDetailId { get; set; }

        public long PotentialId { get; set; }

        [ForeignKey("PotentialId")]
        public virtual PotentialContract Potential { get; set; }
    }
}
