using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Common.Types
{
    [Serializable]
    [Table("COR.Role")]
    public partial class RoleContract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PotentialId { get; set; }

        public byte RoleId { get; set; }

        [ForeignKey("PotentialId")]
        public virtual PotentialContract Potential { get; set; }
    }
}
