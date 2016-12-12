using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace LCLFileUpload.DAL
{
    public class Dal
    {
        public void AddTagRange(Int32 RentalID, Int32 TagValFrom, Int32 TagValTo, string Description)
        {
            SqlDataReader rdr = null;
            Int32 Diff = 0;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spAddTagRange]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RentalID", SqlDbType.BigInt)).Value = RentalID;
                cmd.Parameters.Add(new SqlParameter("@TagValFrom", SqlDbType.VarChar)).Value = TagValFrom;
                cmd.Parameters.Add(new SqlParameter("@TagValTo", SqlDbType.Float)).Value = TagValTo;
                cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar)).Value = Description;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Diff = Int32.Parse(rdr["Diff"].ToString());
                    }
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
        }

        public void AddRentalFile(Int32 RentalID, string TXFileName, Int32 LoginID, bool JobNoMismatch, bool InvalidFile, Int32 Lines, Int32 Qty, Int32 Ext)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spAddRentalFile]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RentalID", SqlDbType.BigInt)).Value = RentalID;
                cmd.Parameters.Add(new SqlParameter("@TXFileName", SqlDbType.VarChar)).Value = TXFileName;
                cmd.Parameters.Add(new SqlParameter("@LoginID", SqlDbType.Int)).Value = LoginID;
                cmd.Parameters.Add(new SqlParameter("@JobNoMismatch", SqlDbType.Bit)).Value = JobNoMismatch;
                cmd.Parameters.Add(new SqlParameter("@InvalidFile", SqlDbType.Bit)).Value = InvalidFile;
                cmd.Parameters.Add(new SqlParameter("@Lines", SqlDbType.BigInt)).Value = Lines;
                cmd.Parameters.Add(new SqlParameter("@Qty", SqlDbType.BigInt)).Value = Qty;
                cmd.Parameters.Add(new SqlParameter("@Ext", SqlDbType.BigInt)).Value = Ext;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
        }

        public void DisableEnableUserLogin(Int32 LoginID)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spDisableEnableUserLogin]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LoginID", SqlDbType.Int)).Value = LoginID;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
        }

        public void EditTagRange(Int32 TagRangeID, Int32 TagValFrom, Int32 TagValTo, string Description)
        {
            SqlDataReader rdr = null;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spEditTagRange]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TagRangeID", SqlDbType.BigInt)).Value = TagRangeID;
                cmd.Parameters.Add(new SqlParameter("@TagValFrom", SqlDbType.VarChar)).Value = TagValFrom;
                cmd.Parameters.Add(new SqlParameter("@TagValTo", SqlDbType.Float)).Value = TagValTo;
                cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar)).Value = Description;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();

                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
        }

        public void DeleteTagRange(Int32 TagRangeID)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spDeleteTagRange]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TagRangeID", SqlDbType.BigInt)).Value = TagRangeID;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();

                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
        }

        public Int32 ValidateUserLogin(string username, string password)
        {
            SqlDataReader rdr = null;
            Int32 id = 0;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spValidateUserLogin]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar)).Value = username.Trim();//Pass the parameter
                cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar)).Value = password.Trim();//Pass the parameter
                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        id = Int32.Parse(rdr["LoginID"].ToString());
                    }
                    return id;
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                    if (rdr != null)
                    {
                        rdr.Close();
                    }
                }
            }
        }

        public Int32 GetRentalID(int JobNo)
        {
            SqlDataReader rdr = null;
            Int32 id = 0;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spGetRentalID]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobNo", SqlDbType.VarChar)).Value = JobNo;//Pass the parameter
                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        id = Int32.Parse(rdr["RentalID"].ToString());
                    }
                    return id;
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                    if (rdr != null)
                    {
                        rdr.Close();
                    }
                }
            }
        }

        public Int32 FindIfTangRangeExist(int JobNo)
        {
            SqlDataReader rdr = null;
            Int32 id = 0;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spFindIfTagRangeExist]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobNo", SqlDbType.VarChar)).Value = JobNo;//Pass the parameter
                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        id = Int32.Parse(rdr["RetValue"].ToString());
                    }
                    return id;
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                    if (rdr != null)
                    {
                        rdr.Close();
                    }
                }
            }
        }

        public DataTable GetListOfJobsForDLPM(int UserID)
        {
            LCLFileUpload.Models.JobListforDLPM LJ = new Models.JobListforDLPM();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spGetJobListForDLPM]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = UserID;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
            return new DataTable();
        }

        public DataTable GetListOfUploadedFiles(int RentalID)
        {
            LCLFileUpload.Models.JobListforDLPM LJ = new Models.JobListforDLPM();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spGetListOfUploadedFiles]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RentalID", SqlDbType.Int)).Value = RentalID;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
            return new DataTable();
        }

        public DataTable GetListOfUploadedFilesErrors(int RentalID)
        {
            LCLFileUpload.Models.JobListforDLPM LJ = new Models.JobListforDLPM();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spGetListOfUploadedFilesErrors]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RentalID", SqlDbType.Int)).Value = RentalID;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
            return new DataTable();
        }

        public DataTable FindIfTagRangeOverlap(int RentalID, int TagValFrom, int TagValTo, string Description)
        {
            LCLFileUpload.Models.JobListforDLPM LJ = new Models.JobListforDLPM();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spFindIfTagRangeOverlap]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RentalID", SqlDbType.Int)).Value = RentalID;
                cmd.Parameters.Add(new SqlParameter("@TagValFrom", SqlDbType.Int)).Value = TagValFrom;
                cmd.Parameters.Add(new SqlParameter("@TagValTo", SqlDbType.Int)).Value = TagValTo;
                cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar)).Value = Description;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
            return new DataTable();
        }

        public DataTable FindIfTagRangeOverlapForEdit(int TagRangeID, int RentalID, int TagValFrom, int TagValTo, string Description)
        {
            LCLFileUpload.Models.JobListforDLPM LJ = new Models.JobListforDLPM();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spFindIfTagRangeOverlapForEdit]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TagRangeID", SqlDbType.Int)).Value = TagRangeID;
                cmd.Parameters.Add(new SqlParameter("@RentalID", SqlDbType.Int)).Value = RentalID;
                cmd.Parameters.Add(new SqlParameter("@TagValFrom", SqlDbType.Int)).Value = TagValFrom;
                cmd.Parameters.Add(new SqlParameter("@TagValTo", SqlDbType.Int)).Value = TagValTo;
                cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar)).Value = Description;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
            return new DataTable();
        }

        public Int32 UpdateRentalJobStaus(Int32 JobNo, Int32 LoginID)
        {
            SqlDataReader rdr = null;
            Int32 id = 0;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spUpdateRentalJobStaus]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobNo", SqlDbType.BigInt)).Value = JobNo;
                cmd.Parameters.Add(new SqlParameter("@LoginID", SqlDbType.BigInt)).Value = LoginID;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        id = Int32.Parse(rdr["RetValue"].ToString());
                    }
                    return id;
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                    if (rdr != null)
                    {
                        rdr.Close();
                    }
                }
            }
        }
       
        public void UpdateDLPMStoreLink(Int32 RecID, string StoreNo)
        {

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spUpdateDLPMStoreLink]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RecID", SqlDbType.BigInt)).Value = RecID;
                cmd.Parameters.Add(new SqlParameter("@StoreNo", SqlDbType.Char)).Value = StoreNo;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
        }

        public void DeleteDLPMStoreLink(Int32 RecID)
        {

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spDeleteDLPMStoreLink]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RecID", SqlDbType.BigInt)).Value = RecID;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
        }

        public void AddDLPMStoreLink(long UserID, string StoreNo, string PhoneNo)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spAddDLPMStoreLink]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.BigInt)).Value = UserID;
                cmd.Parameters.Add(new SqlParameter("@StoreNo", SqlDbType.Char)).Value = StoreNo;
                cmd.Parameters.Add(new SqlParameter("@PhoneNo", SqlDbType.Char)).Value = PhoneNo;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
        }

        public string GetDLPMEmail(long UserID)
        {
            SqlDataReader rdr = null;
            string id = null;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spGetDLPMEmail]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.BigInt)).Value = UserID;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        id = rdr["PhoneNo"].ToString();
                    }
                    return id;
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                    if (rdr != null)
                    {
                        rdr.Close();
                    }
                }
            }
        }

        public void AddUpdateJobNotes(int JobNo, string Notes)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString))
            {
                var cmd = new SqlCommand("[spAddUpdateRentalJobNotes]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobNo", SqlDbType.Int)).Value = JobNo;
                cmd.Parameters.Add(new SqlParameter("@Notes", SqlDbType.VarChar)).Value = Notes;

                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
        }


    }
}