using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Common.Types
{
    [Serializable]
    public abstract class ContactBase:ContractBase
    {
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

        [ForeignKey("AddressId")]
        public virtual AddressContact Address { get; set; }

        [ForeignKey("Address2Id")]
        public virtual AddressContact Address2 { get; set; }

        [ForeignKey("EmailId")]
        public virtual EmailContact Email { get; set; }

        [ForeignKey("FaxId")]
        public virtual FaxContact Fax { get; set; }

        [ForeignKey("PhoneId")]
        public virtual PhoneContact Phone { get; set; }
    }

    [Serializable]
    [Table("CompanyContact", Schema = "CNT")]
    public class CompanyContactContract:ContactBase
    {
        [Key]
        public long CompanyContactId { get; set; }
        public long CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual CompanyContract Company { get; set; }

    }
    [Serializable]
    [Table("PotentialContact", Schema = "CNT")]
    public class PotentialContactContract : ContactBase
    {
        [Key]
        public long ContactId { get; set; }

        public long PotentialId { get; set; }

        [ForeignKey("PotentialId")]
        public virtual PotentialContract Potential { get; set; }
    }

    [Serializable]
    [Table("Phone", Schema = "CNT")]
    public class PhoneContact : ContractBase
    {
        [Key]
        public int PhoneId { get; set; }

        [Required]
        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Phone2 { get; set; }
    }

    [Serializable]
    [Table("Fax", Schema = "CNT")]
    public class FaxContact : ContractBase
    {
        [Key]
        public long FaxId { get; set; }

        [Required]
        [StringLength(24)]
        public string Fax { get; set; }

        [StringLength(24)]
        public string Fax2 { get; set; }
    }
    
    [Serializable]
    [Table("CNT.Address")]
    public class AddressContact : ContractBase
    {
        [Key]
        public long AddressId { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Region { get; set; }

        [StringLength(50)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }
        
    }

    [Serializable]
    [Table("Email", Schema = "CNT")]
    public class EmailContact : ContractBase
    {
        [Key]
        public int EmailId { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Email2 { get; set; }
        
    }
}
