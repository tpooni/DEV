//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LCLFileUpload.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblStatusInfo
    {
        public tblStatusInfo()
        {
            this.tblJobStatus = new HashSet<tblJobStatu>();
        }
    
        public int Staus { get; set; }
        public string StatusName { get; set; }
        public System.DateTime LastEdit { get; set; }
        public string LastEditor { get; set; }
    
        public virtual ICollection<tblJobStatu> tblJobStatus { get; set; }
    }
}