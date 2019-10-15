namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COR.EmployeeDetail")]
    public partial class EmployeeDetail
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

        public virtual Department Department { get; set; }
    }
}
