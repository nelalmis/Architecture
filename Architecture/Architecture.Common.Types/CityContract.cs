using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Common.Types
{
    [Serializable]
    [Table("City", Schema = "CNT")]
    public partial class CityContract: ContractBase
    {
        public CityContract()
        {
            CountyList = new HashSet<CountyContract>();
        }
        [Key]
        public short CityId { get; set; }

        [StringLength(50)]
        public string CityName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CountyContract> CountyList { get; set; }
    }
}
