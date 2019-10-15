using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Common.Types
{
    [Serializable]
    [Table("County", Schema = "CNT")]
    public partial class CountyContract: ContractBase
    {
        [Key]
        public short CountyId { get; set; }

        [StringLength(50)]
        public string CountyName { get; set; }

        public short? CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual CityContract City { get; set; }
    }
}
