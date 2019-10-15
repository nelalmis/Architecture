using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Common.Types
{
    [Serializable]
    [Table("Country", Schema = "CNT")]
    public partial class CountryContract : ContractBase
    {
        [Key]
        [Column(Order = 0)]
        public int CountryId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CountryCode { get; set; }

        [StringLength(150)]
        public string CountryName { get; set; }
    }
}
