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
    
    public partial class Parameter
    {
        public int ParamId { get; set; }
        public long CompanyId { get; set; }
        public string ParamType { get; set; }
        public string ParamCode { get; set; }
        public string ParamDescription { get; set; }
        public Nullable<int> ParamValue { get; set; }
        public string ParamValue2 { get; set; }
        public string ParamValue3 { get; set; }
        public string ParamValue4 { get; set; }
        public string ParamValue5 { get; set; }
        public string ParamValue6 { get; set; }
        public string ParamValue7 { get; set; }
        public string UserName { get; set; }
        public string HostName { get; set; }
        public Nullable<System.DateTime> SystemDate { get; set; }
        public string UpdateUserName { get; set; }
        public Nullable<System.DateTime> UpdateSystemDate { get; set; }
        public string HostIp { get; set; }
    
        public virtual Company Company { get; set; }
    }
}