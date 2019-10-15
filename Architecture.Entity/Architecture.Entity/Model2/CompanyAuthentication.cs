namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COR.CompanyAuthentication")]
    public partial class CompanyAuthentication
    {
        public long CompanyAuthenticationId { get; set; }

        public long CompanyId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        public virtual Company Company { get; set; }
    }
}
