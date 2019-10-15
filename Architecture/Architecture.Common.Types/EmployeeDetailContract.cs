using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Common.Types
{
    [Serializable]
    [Table("EmployeeDetail", Schema = "COR")]
    public class EmployeeDetailContract
    {
        [Key]
        [Column(Order = 0)]
        public int EmployeeDetailId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PotentialId { get; set; }

        public int DepartmentId { get; set; }

        public decimal? Salary { get; set; }

        public int? SuperPotentialId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public byte? Status { get; set; }

        [ForeignKey("PotentialId")]
        public virtual PotentialContract Potential { get; set; }

        [ForeignKey("SuperPotentialId")]
        public virtual PotentialContract SuperPotential { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual DepartmentContract Department { get; set; }

    }
}
