namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COR.Authentication")]
    public partial class Authentication
    {
        public long AuthenticationId { get; set; }

        public long PotentialId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        public virtual Potential Potential { get; set; }
    }
}
