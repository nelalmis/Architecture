using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Common.Types
{
    [Serializable]
    [Table("UserDetail", Schema = "COR")]
    public class UserDetailContract
    {
        [Key]
        public int UserDetailId { get; set; }
        public long PotentialId { get; set; }

        [ForeignKey("PotentailId")]
        public virtual PotentialContract Potential { get; set; }
    }
}
