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
    
    public partial class tblLoginInfo
    {
        public tblLoginInfo()
        {
            this.webpages_Roles = new HashSet<webpages_Roles>();
        }
    
        public int LoginID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Deleted { get; set; }
        public System.DateTime LastEdit { get; set; }
        public string LastEditor { get; set; }
    
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}