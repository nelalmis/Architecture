namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COR.ErrorCodes")]
    public partial class ErrorCodes
    {
        [Key]
        [Column(Order = 0)]
        public int ErrorCode { get; set; }

        [StringLength(500)]
        public string ErrorDescription { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte Severity { get; set; }

        [StringLength(10)]
        public string UserName { get; set; }

        [StringLength(20)]
        public string HostName { get; set; }

        public DateTime? SystemDate { get; set; }

        public DateTime? UpdateUserName { get; set; }

        public DateTime? UpdateSystemDate { get; set; }

        [StringLength(15)]
        public string HosIp { get; set; }
    }
}
