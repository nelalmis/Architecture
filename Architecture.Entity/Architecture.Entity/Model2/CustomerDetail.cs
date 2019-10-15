namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COR.CustomerDetail")]
    public partial class CustomerDetail
    {
        public int CustomerDetailId { get; set; }

        public long PotentialId { get; set; }

        public virtual Potential Potential { get; set; }
    }
}
