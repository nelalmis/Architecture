namespace Architecture.Entity.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CNT.PotentialContact")]
    public partial class PotentialContact
    {
        [Key]
        public long ContactId { get; set; }

        public long PotentialId { get; set; }

        [Required]
        [StringLength(30)]
        public string ContactName { get; set; }

        [Required]
        [StringLength(30)]
        public string ContactTitle { get; set; }

        public long? AddressId { get; set; }

        public long? Address2Id { get; set; }

        public long? EmailId { get; set; }

        public long? PhoneId { get; set; }

        public long? FaxId { get; set; }

        public virtual Address Address { get; set; }

        public virtual Address Address1 { get; set; }

        public virtual Email Email { get; set; }

        public virtual Fax Fax { get; set; }

        public virtual Phone Phone { get; set; }

        public virtual Potential Potential { get; set; }
    }
}
