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
    
    public partial class Message
    {
        public int MessageId { get; set; }
        public long CompanyId { get; set; }
        public string MessageCode { get; set; }
        public string GroupName { get; set; }
        public string ClassName { get; set; }
        public string PropertyName { get; set; }
        public string MessageText { get; set; }
        public Nullable<byte> LanguageId { get; set; }
    
        public virtual Company Company { get; set; }
    }
}
