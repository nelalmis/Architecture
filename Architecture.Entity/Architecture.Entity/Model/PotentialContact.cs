//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Architecture.Entity.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PotentialContact
    {
        public long ContactId { get; set; }
        public long PotentialId { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public Nullable<long> AddressId { get; set; }
        public Nullable<long> Address2Id { get; set; }
        public Nullable<long> EmailId { get; set; }
        public Nullable<long> PhoneId { get; set; }
        public Nullable<long> FaxId { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Address Address1 { get; set; }
        public virtual Email Email { get; set; }
        public virtual Fax Fax { get; set; }
        public virtual Phone Phone { get; set; }
        public virtual Potential Potential { get; set; }
    }
}
