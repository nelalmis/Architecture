//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Types
{
    using System;
    using System.Collections.Generic;
    [Serializable]
    public partial class dept_locations
    {
        public short dnumber { get; set; }
        public string dlocation { get; set; }
    
        public virtual department department { get; set; }
    }
}