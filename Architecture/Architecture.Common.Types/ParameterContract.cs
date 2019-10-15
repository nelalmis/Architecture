using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Common.Types
{
    [Serializable]
    [Table("Parameter", Schema = "COR")]
    public class ParameterContract : ContractBase
    {
        [Key]
        public int ParamId { get; set; }

        public long CompanyId { get; set; }

        [Required]
        [StringLength(50)]
        public string ParamType { get; set; }

        [Required]
        [StringLength(50)]
        public string ParamCode { get; set; }

        [StringLength(50)]
        public string ParamDescription { get; set; }

        public int? ParamValue { get; set; }

        [StringLength(50)]
        public string ParamValue2 { get; set; }

        [StringLength(50)]
        public string ParamValue3 { get; set; }

        [StringLength(50)]
        public string ParamValue4 { get; set; }

        [StringLength(50)]
        public string ParamValue5 { get; set; }

        [StringLength(50)]
        public string ParamValue6 { get; set; }

        [StringLength(50)]
        public string ParamValue7 { get; set; }

        [StringLength(10)]
        public string UserName { get; set; }

        [StringLength(20)]
        public string HostName { get; set; }

        public DateTime? SystemDate { get; set; }

        [StringLength(10)]
        public string UpdateUserName { get; set; }

        public DateTime? UpdateSystemDate { get; set; }

        [StringLength(15)]
        public string HostIp { get; set; }

        [ForeignKey("CompanyId")]
        public virtual CompanyContract Company { get; set; }
    }

}
