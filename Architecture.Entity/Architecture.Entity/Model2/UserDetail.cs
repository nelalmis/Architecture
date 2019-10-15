namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COR.UserDetail")]
    public partial class UserDetail
    {
        public int UserDetailId { get; set; }

        public long PotentialId { get; set; }

        public virtual Potential Potential { get; set; }
    }
}
