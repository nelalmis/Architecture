namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CNT.Country")]
    public partial class Country
    {
        [Key]
        [Column(Order = 0)]
        public int CountyId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CountyCode { get; set; }

        [StringLength(150)]
        public string CountryName { get; set; }
    }
}
