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
    
    public partial class Authentication
    {
        public long AuthenticationId { get; set; }
        public long PotentialId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    
        public virtual Potential Potential { get; set; }
    }
}
