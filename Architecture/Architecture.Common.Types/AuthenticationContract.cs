using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Common.Types
{
    [Serializable]
    [Table("Authentication", Schema = "COR")]
    public class AuthenticationContract : ContractBase
    {
        [Key]
        public long AuthenticationId { get; set; }
        public long PotentialId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }
        public bool IsAuthentication { get; set; }

        [ForeignKey("PotentialId")]
        public virtual PotentialContract Potential { get; set; }

    }

    [Serializable]
    [Table("COR.CompanyAuthentication")]
    public partial class CompanyAuthenticationContract:ContractBase
    {
        [Key]
        public long CompanyAuthenticationId { get; set; }

        public long CompanyId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [ForeignKey("CompanyId")]
        public virtual CompanyContract Company { get; set; }
    }
}
