namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CNT.County")]
    public partial class County
    {
        public short CountyId { get; set; }

        [StringLength(50)]
        public string CountyName { get; set; }

        public short? CityId { get; set; }

        public virtual City City { get; set; }
    }
}
