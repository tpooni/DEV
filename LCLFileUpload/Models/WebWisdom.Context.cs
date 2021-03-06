﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class WEBWISDOMEntities : DbContext
    {
        public WEBWISDOMEntities()
            : base("name=WEBWISDOMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tblJobStatu> tblJobStatus { get; set; }
        public DbSet<tblLoginInfo> tblLoginInfoes { get; set; }
        public DbSet<tblRentalFileInfo> tblRentalFileInfoes { get; set; }
        public DbSet<tblRentalJobInfo> tblRentalJobInfoes { get; set; }
        public DbSet<tblRentalTagRange> tblRentalTagRanges { get; set; }
        public DbSet<tblStatusInfo> tblStatusInfoes { get; set; }
        public DbSet<tblRentalDLPMStoreLink> tblRentalDLPMStoreLinks { get; set; }
        public DbSet<tblUserInfo> tblUserInfoes { get; set; }
        public DbSet<webpages_Membership> webpages_Membership { get; set; }
        public DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public DbSet<webpages_Roles> webpages_Roles { get; set; }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int spAddRentalFile(Nullable<int> rentalID, string tXFileName, Nullable<int> loginID, Nullable<bool> jobNoMismatch, Nullable<bool> invalidFile)
        {
            var rentalIDParameter = rentalID.HasValue ?
                new ObjectParameter("RentalID", rentalID) :
                new ObjectParameter("RentalID", typeof(int));
    
            var tXFileNameParameter = tXFileName != null ?
                new ObjectParameter("TXFileName", tXFileName) :
                new ObjectParameter("TXFileName", typeof(string));
    
            var loginIDParameter = loginID.HasValue ?
                new ObjectParameter("LoginID", loginID) :
                new ObjectParameter("LoginID", typeof(int));
    
            var jobNoMismatchParameter = jobNoMismatch.HasValue ?
                new ObjectParameter("JobNoMismatch", jobNoMismatch) :
                new ObjectParameter("JobNoMismatch", typeof(bool));
    
            var invalidFileParameter = invalidFile.HasValue ?
                new ObjectParameter("InvalidFile", invalidFile) :
                new ObjectParameter("InvalidFile", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spAddRentalFile", rentalIDParameter, tXFileNameParameter, loginIDParameter, jobNoMismatchParameter, invalidFileParameter);
        }
    
        public virtual int spAddTagRange(Nullable<int> rentalID, Nullable<int> tagValFrom, Nullable<int> tagValTo, string description)
        {
            var rentalIDParameter = rentalID.HasValue ?
                new ObjectParameter("RentalID", rentalID) :
                new ObjectParameter("RentalID", typeof(int));
    
            var tagValFromParameter = tagValFrom.HasValue ?
                new ObjectParameter("TagValFrom", tagValFrom) :
                new ObjectParameter("TagValFrom", typeof(int));
    
            var tagValToParameter = tagValTo.HasValue ?
                new ObjectParameter("TagValTo", tagValTo) :
                new ObjectParameter("TagValTo", typeof(int));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spAddTagRange", rentalIDParameter, tagValFromParameter, tagValToParameter, descriptionParameter);
        }
    
        public virtual int spDeleteTagRange(Nullable<int> tagRangeID)
        {
            var tagRangeIDParameter = tagRangeID.HasValue ?
                new ObjectParameter("TagRangeID", tagRangeID) :
                new ObjectParameter("TagRangeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spDeleteTagRange", tagRangeIDParameter);
        }
    
        public virtual int spEditTagRange(Nullable<int> tagRangeID, Nullable<int> tagValFrom, Nullable<int> tagValTo, string description)
        {
            var tagRangeIDParameter = tagRangeID.HasValue ?
                new ObjectParameter("TagRangeID", tagRangeID) :
                new ObjectParameter("TagRangeID", typeof(int));
    
            var tagValFromParameter = tagValFrom.HasValue ?
                new ObjectParameter("TagValFrom", tagValFrom) :
                new ObjectParameter("TagValFrom", typeof(int));
    
            var tagValToParameter = tagValTo.HasValue ?
                new ObjectParameter("TagValTo", tagValTo) :
                new ObjectParameter("TagValTo", typeof(int));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spEditTagRange", tagRangeIDParameter, tagValFromParameter, tagValToParameter, descriptionParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> spFindIfTagRangeExist(Nullable<int> jobNo)
        {
            var jobNoParameter = jobNo.HasValue ?
                new ObjectParameter("JobNo", jobNo) :
                new ObjectParameter("JobNo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("spFindIfTagRangeExist", jobNoParameter);
        }
    
        public virtual ObjectResult<spGetJobListForDLPM_Result> spGetJobListForDLPM(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetJobListForDLPM_Result>("spGetJobListForDLPM", userIDParameter);
        }
    
        public virtual ObjectResult<spGetListOfUploadedFiles_Result> spGetListOfUploadedFiles(Nullable<int> rentalID)
        {
            var rentalIDParameter = rentalID.HasValue ?
                new ObjectParameter("RentalID", rentalID) :
                new ObjectParameter("RentalID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetListOfUploadedFiles_Result>("spGetListOfUploadedFiles", rentalIDParameter);
        }
    
        public virtual ObjectResult<spGetLoginInfo_Result> spGetLoginInfo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetLoginInfo_Result>("spGetLoginInfo");
        }
    
        public virtual ObjectResult<Nullable<int>> spGetRentalID(Nullable<int> jobNo)
        {
            var jobNoParameter = jobNo.HasValue ?
                new ObjectParameter("JobNo", jobNo) :
                new ObjectParameter("JobNo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("spGetRentalID", jobNoParameter);
        }
    }
}
