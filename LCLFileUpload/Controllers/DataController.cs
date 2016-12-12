using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rebex.Net;
using DevExpress.Web.Mvc;
using LCLFileUpload.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.UI.WebControls;
using System.Xml;
using WebMatrix.WebData;
using System.Configuration;
using System.IO;

namespace LCLFileUpload.Controllers
{
    public class DataController : Controller
    {
        LCLFileUpload.Models.WEBWISDOMEntities db = new LCLFileUpload.Models.WEBWISDOMEntities();
        LCLFileUpload.DAL.Dal dal = new LCLFileUpload.DAL.Dal();

        //
        // GET: /Data/

        public ActionResult TagRanges()
        {
            if (!WebSecurity.Initialized)
            {
                try
                {
                    WebSecurity.InitializeDatabaseConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString, "System.Data.SqlClient", "tblUserInfo", "LoginID", "UserName", autoCreateTables: true);
                }
                catch { }
            }
            var f = new JobListforDLPM();
            f.JobList = dal.GetListOfJobsForDLPM(WebSecurity.CurrentUserId);
            return View(f); 
        }

        public ActionResult DownloadData()
        {
            if (!WebSecurity.Initialized)
            {
                try
                {
                    WebSecurity.InitializeDatabaseConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString, "System.Data.SqlClient", "tblUserInfo", "LoginID", "UserName", autoCreateTables: true);
                }
                catch { }
            }
            var f = new JobListforDLPM();
            f.JobList = dal.GetListOfJobsForDLPM(WebSecurity.CurrentUserId);
            return View(f);
        }

        [HttpPost]
        public ActionResult DownloadData(HttpPostedFileBase[] files)
        {
            try
            {
                Sftp client = new Sftp();

                string hostname = System.Configuration.ConfigurationManager.AppSettings["hostname"];
                string userName = System.Configuration.ConfigurationManager.AppSettings["username"].ToString();
                string password = System.Configuration.ConfigurationManager.AppSettings["password"].ToString();
                client.Connect(hostname);
                client.Login(userName, password);
                client.ChangeDirectory("TerminalFiles");

                /*Lopp for multiple files*/
                foreach (HttpPostedFileBase file in files)
                {
                    /*Geting the file name*/

                    string filename = System.IO.Path.GetFileName(file.FileName);
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    client.PutFile(ms, filename);
                }
                client.Disconnect();
                ViewBag.Message = "File Uploaded successfully.";
            }
            catch
            {
                ViewBag.Message = "Error while uploading the files.";
            }
            return View(); return RedirectToAction("Index");
        }

        [ValidateInput(false)]
        public ActionResult _grdEnterTagRanges(int jobNo)
        {
            int RentalID = dal.GetRentalID(jobNo);
            var model = db.tblRentalTagRanges;
/*            if (model.Count() > 0) 
                return PartialView("_grdEnterTagRanges", model.ToList().Where(t => !(t.Deleted)));
            else
            {
                var f = new tblRentalTagRange();
                f.RentalID = RentalID;
                return PartialView(f);
            } */
                        return PartialView("_grdEnterTagRanges", model.ToList().Where(t => !(t.Deleted) && (t.RentalID == RentalID)));
        }

        [ValidateInput(false)]
        public ActionResult EnterTagRanges(int jobNo)
        {
            return View();
//            int RentalID = dal.GetRentalID(jobNo);
  //          var model = db.tblRentalTagRanges;
            //            return PartialView("_grdEnterTagRanges", model.ToList().Where(t => !(t.Deleted) && (t.RentalID == RentalID)));
    //        return PartialView("_grdEnterTagRanges", model.ToList());
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _grdEnterTagRanges([ModelBinder(typeof(DevExpressEditorsBinder))] LCLFileUpload.Models.tblRentalTagRange item)
        {
            var model = db.tblRentalTagRanges;
//            var f = new tblRentalTagRange();
            if (ModelState.IsValid) dal.AddTagRange(item.RentalID, item.TagValFrom, item.TagValTo, item.Description);
            return PartialView("_grdEnterTagRanges", model.ToList().Where(t => !(t.Deleted) && (t.RentalID == item.RentalID)));
//            return PartialView("_grdEnterTagRanges", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult _ListTagRangesPartial(int jobNo)
        {
            int RentalID = dal.GetRentalID(jobNo);

            var model = db.tblRentalTagRanges;
            return PartialView(model.ToList().Where(t => !(t.Deleted) && (t.RentalID == RentalID)));
        }

        [ValidateInput(false)]
        public ActionResult _EnterTagRangesPartial(int jobNo)
        {
            int RentalID = dal.GetRentalID(jobNo);
            Int32 tempvalue = 0;
            ViewData["TagRangeID"] = tempvalue.ToString();
            var f = new tblRentalTagRange();
            f.RentalID = RentalID;
            return PartialView(f);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _EnterTagRangesPartial(tblRentalTagRange tblrentalragrange)
        {
            if (ModelState.IsValid)
            {
                if (tblrentalragrange.TagValTo >= tblrentalragrange.TagValFrom)
                {
                    if (dal.FindIfTagRangeOverlap(tblrentalragrange.RentalID, tblrentalragrange.TagValFrom, tblrentalragrange.TagValTo, tblrentalragrange.Description).Rows.Count == 0)
                    {
                        dal.AddTagRange(tblrentalragrange.RentalID, tblrentalragrange.TagValFrom, tblrentalragrange.TagValTo, tblrentalragrange.Description);
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "ERROR: Tag Range or Description already exist, Please try again.";
                    }
                }
                else
                {
                    ViewBag.Message = "ERROR: Tag Range value 'To' is less than 'From', Please try again.";
                }
            }
            Int32 tempvalue = 0;
            ViewData["TagRangeID"] = tempvalue.ToString();
            var f = new tblRentalTagRange();
            f.RentalID = tblrentalragrange.RentalID;
            return PartialView(f);
        }

        [ValidateInput(false)]
        public ActionResult _EditTagRange(int JobNo, int TagRangeID)
        {
            tblRentalTagRange tblrentaltagrange = db.tblRentalTagRanges.Find(TagRangeID);
            return View(tblrentaltagrange);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _EditTagRange(int JobNo, tblRentalTagRange tblrentalragrange)
        {
            if (tblrentalragrange.TagValTo >= tblrentalragrange.TagValFrom)
            {
                if (dal.FindIfTagRangeOverlapForEdit(tblrentalragrange.TagRangeID, tblrentalragrange.RentalID, tblrentalragrange.TagValFrom, tblrentalragrange.TagValTo, tblrentalragrange.Description).Rows.Count == 0)
                {
                    dal.EditTagRange(tblrentalragrange.TagRangeID, tblrentalragrange.TagValFrom, tblrentalragrange.TagValTo, tblrentalragrange.Description);
                    return RedirectToAction("EnterTagRanges", new { JobNo = JobNo });
                }
                else
                {
                    ViewBag.Message = "ERROR: Tag Range or Description already exist, Please try again.";
                }
            }
            else
            {
                ViewBag.Message = "ERROR: Tag Range value 'To' is less than 'From', Please try again.";
            }
            tblRentalTagRange tblrentaltagrange = db.tblRentalTagRanges.Find(tblrentalragrange.TagRangeID);
            return View(tblrentaltagrange);
        }

        [ValidateInput(false)]
        public ActionResult _DeleteTagRange(int JobNo, int TagRangeID)
        {
            tblRentalTagRange tblrentaltagrange = db.tblRentalTagRanges.Find(TagRangeID);
            return View(tblrentaltagrange);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _DeleteTagRange(int JobNo, tblRentalTagRange tblrentalragrange)
        {
            dal.DeleteTagRange(tblrentalragrange.TagRangeID);
            return RedirectToAction("EnterTagRanges", new { JobNo = JobNo });
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _grdNewEnterTagRanges([ModelBinder(typeof(DevExpressEditorsBinder))] LCLFileUpload.Models.tblRentalTagRange item)
        {
            var model = db.tblRentalTagRanges;
            var f = new tblRentalTagRange(); 
            return PartialView(f);
        }
         
        [ValidateInput(false)]
        public ActionResult _UploadFilesPartial(int JobNo)
        {
            Sftp client = new Sftp();
            string hostname = System.Configuration.ConfigurationManager.AppSettings["hostname"];
            string userName = System.Configuration.ConfigurationManager.AppSettings["username"].ToString();
            string password = System.Configuration.ConfigurationManager.AppSettings["password"].ToString();
            
            client.Connect(hostname);
            client.Login(userName, password);
            client.ChangeDirectory("TerminalFiles");

            bool exist = (client.DirectoryExists(JobNo.ToString()));
            
            if (!exist)
            {
                client.CreateDirectory(JobNo.ToString());
            }
            if (dal.FindIfTangRangeExist(JobNo) == 0)
            {
                ViewBag.Message = "ERROR: Missing Tag Ranges, Please Enter Ranges First.";
            }
            return View();
        }

        /*
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _UploadFilesPartial(int JobNo, HttpPostedFileBase[] files)
        {
            try
            {
                int RentalID = dal.GetRentalID(JobNo);
                Sftp client = new Sftp();

                string hostname = System.Configuration.ConfigurationManager.AppSettings["hostname"];
                string userName = System.Configuration.ConfigurationManager.AppSettings["username"].ToString();
                string password = System.Configuration.ConfigurationManager.AppSettings["password"].ToString();
                client.Connect(hostname);
                client.Login(userName, password);
                client.ChangeDirectory("inbox");
                client.ChangeDirectory(JobNo.ToString());

                foreach (HttpPostedFileBase file in files)
                {
                    bool InvalidFile = false;
                    bool JobNoMismatch = false;

                    string filename = System.IO.Path.GetFileName(file.FileName);
                    if (System.IO.Path.GetExtension(filename).ToLower() == ".t750")
                    {
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        //                    System.IO.MemoryStream ms2 = new System.IO.MemoryStream(fileBytes);
                        //                    client.PutFile(ms, filename);

                        //                    StreamReader reading = System.IO.File.OpenText("E:\test.txt");
                        //                  string str;
                        //                while ((str = reading.ReadLine()) != null)
                        //              {
                        XmlDocument xml = new XmlDocument();
                        StreamReader sr = new StreamReader(ms);
                        string xmlString = sr.ReadToEnd();
                        xml.LoadXml(xmlString); // suppose that myXmlString contains "<Names>...</Names>"

                        XmlNode N = xml.SelectSingleNode("NMTPEnvelope/payload/countSeg");
                        if (N.Attributes["jn"].Value.ToString() != JobNo.ToString())
                        {
                            JobNoMismatch = true;
                            //                        xmlString.Replace(N.Attributes["jn"].Value.ToString(), JobNo.ToString());
                            //                      xml.Save(ms2);
                        }
                        client.PutFile(ms, filename);

                        //                        XmlDocument D;
                        //                      XmlNode N = D.SelectSingleNode(@"\NMTPEnvelope\payload\countSeg");
                        //                    N.Attributes["jn"] = NewJobNo;

                        //                  D.SelectSingleNode(@"\NMTPEnvelope\payload\TagCompleteReq\Hash").Value = BuildHash(N.OuterXml);



                        //                        if (str.Contains("some text"))
                        //                      {
                        //                        StreamWriter write = new StreamWriter("test.txt");
                        //                  }
                        //            }
                    }
                    else
                    {
                        InvalidFile = true;
                    }
                    dal.AddRentalFile(RentalID, filename, WebSecurity.CurrentUserId, JobNoMismatch, InvalidFile);
                }
                client.Disconnect();
                ViewBag.Message = "File Uploaded successfully.";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error while uploading the files." + ex.Message;
            }
            return View(); 
            //return RedirectToAction("Index");
        }*/

        [ValidateInput(false)]
        public ActionResult _ListUploadedFilesPartial(int JobNo)
        {
            if (!WebSecurity.Initialized)
            {
                try
                {
                    WebSecurity.InitializeDatabaseConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString, "System.Data.SqlClient", "tblUserInfo", "LoginID", "UserName", autoCreateTables: true);
                }
                catch { }
            }
            int RentalID = dal.GetRentalID(JobNo);
            var f = new ListOfUploadedFiles();
            f.FileList = dal.GetListOfUploadedFiles(RentalID);
            return View(f);
        }

        [ValidateInput(false)]
        public ActionResult _ListUploadedFilesErrorsPartial(int JobNo)
        {
            if (!WebSecurity.Initialized)
            {
                try
                {
                    WebSecurity.InitializeDatabaseConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString, "System.Data.SqlClient", "tblUserInfo", "LoginID", "UserName", autoCreateTables: true);
                }
                catch { }
            }
            int RentalID = dal.GetRentalID(JobNo);
            var f = new ListOfUploadedFilesErrors();
            f.ErrorList = dal.GetListOfUploadedFilesErrors(RentalID);
            return View(f);
        }

        [ValidateInput(false)]
        public ActionResult _DownloadDataPartial(int JobNo)
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult UploadFiles(int JobNo)
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UploadFiles(int JobNo, HttpPostedFileBase[] files)
        {
            string filename = "";
            if (!WebSecurity.Initialized)
            {
                try
                {
                    WebSecurity.InitializeDatabaseConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString, "System.Data.SqlClient", "tblUserInfo", "LoginID", "UserName", autoCreateTables: true);
                }
                catch { }
            }
            if (dal.FindIfTangRangeExist(JobNo) == 0)
            {
                ViewBag.Message = "ERROR: Missing tag ranges, No files were uploaded. Please enter tag ranges first.";
                return View();
            } else {
                try
                {
                    int RentalID = dal.GetRentalID(JobNo);
                    Sftp client = new Sftp();

                    string hostname = System.Configuration.ConfigurationManager.AppSettings["hostname"];
                    string userName = System.Configuration.ConfigurationManager.AppSettings["username"].ToString();
                    string password = System.Configuration.ConfigurationManager.AppSettings["password"].ToString();
                    client.Connect(hostname);
                    client.Login(userName, password);
                    client.ChangeDirectory("TerminalFiles");
                    client.ChangeDirectory(JobNo.ToString());

                    /*Lopp for multiple files*/
                    foreach (HttpPostedFileBase file in files)
                    {
                        /*Geting the file name*/
                        bool InvalidFile = false;
                        bool JobNoMismatch = true;

                        filename = System.IO.Path.GetFileName(file.FileName);
                        Int32 Lines = 0, Qty =  0, Ext = 0;
                        if (System.IO.Path.GetExtension(filename).ToLower() == ".750idle") continue;

                        if (System.IO.Path.GetExtension(filename).ToLower() == ".t750")
                        {
                            string fileContentType = file.ContentType;
                            byte[] fileBytes = new byte[file.ContentLength];
                            file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                            System.IO.MemoryStream ms = new System.IO.MemoryStream(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                            client.PutFile(ms, filename);
                            System.IO.MemoryStream ms2 = new System.IO.MemoryStream(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                            //                    client.PutFile(ms, filename);

                            //                    StreamReader reading = System.IO.File.OpenText("E:\test.txt");
                            //                  string str;
                            //                while ((str = reading.ReadLine()) != null)
                            //              {
                            XmlDocument xml = new XmlDocument();
                            StreamReader sr = new StreamReader(ms2);
                            string xmlString = sr.ReadToEnd();
                            xml.LoadXml(xmlString); // suppose that myXmlString contains "<Names>...</Names>"
                            XmlNode N1 = xml.SelectSingleNode("NMTPEnvelope/payload/TagCompleteReq/TagTotals/Rows/Row/Cells");

                            Lines = Int32.Parse(N1.ChildNodes[1].InnerXml);
                            Qty = Int32.Parse(N1.ChildNodes[2].InnerXml);
                            Ext = Int32.Parse(N1.ChildNodes[3].InnerXml);

                            XmlNode N = xml.SelectSingleNode("NMTPEnvelope/payload/countSeg");
                            if (N.Attributes["jn"].Value.ToString() != JobNo.ToString())
                            {
                                JobNoMismatch = true;
                                //                        xmlString.Replace(N.Attributes["jn"].Value.ToString(), JobNo.ToString());
                                //                      xml.Save(ms2);
                            }
                            else
                            {
                                JobNoMismatch = false;
                            }

                            //                        XmlDocument D;
                            //                      XmlNode N = D.SelectSingleNode(@"\NMTPEnvelope\payload\countSeg");
                            //                    N.Attributes["jn"] = NewJobNo;

                            //                  D.SelectSingleNode(@"\NMTPEnvelope\payload\TagCompleteReq\Hash").Value = BuildHash(N.OuterXml);



                            //                        if (str.Contains("some text"))
                            //                      {
                            //                        StreamWriter write = new StreamWriter("test.txt");
                            //                  }
                            //            }
                        }
                        else
                        {
                            InvalidFile = true;
                        }
                        dal.AddRentalFile(RentalID, filename, WebSecurity.CurrentUserId, JobNoMismatch, InvalidFile, Lines, Qty, Ext);
                    }
                    client.Disconnect();
                    ViewBag.Message = "File Uploaded successfully.";
                 }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error: " + ex.Message + "File: " + filename;
                }
                return View(); 
                //return RedirectToAction("Index");
            }
        }

        [ValidateInput(false)]
        public ActionResult Conformation()
        {
            if (!WebSecurity.Initialized)
            {
                try
                {
                    WebSecurity.InitializeDatabaseConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString, "System.Data.SqlClient", "tblUserInfo", "LoginID", "UserName", autoCreateTables: true);
                }
                catch { }
            }
            var f = new JobListforDLPM();
            f.JobList = dal.GetListOfJobsForDLPM(WebSecurity.CurrentUserId);
            return View(f);
        }

        [ValidateInput(false)]
        public ActionResult ConfirmJob(int JobNo)
        {
            if (!WebSecurity.Initialized)
            {
                try
                {
                    WebSecurity.InitializeDatabaseConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString, "System.Data.SqlClient", "tblUserInfo", "LoginID", "UserName", autoCreateTables: true);
                }
                catch { }
            }
            if (JobNo > 0)
            {
                if (dal.UpdateRentalJobStaus(JobNo, WebSecurity.CurrentUserId) == 0)
                    ViewBag.Message = "Tag range or no data exist for this job.";
                else
                    ViewBag.Message = "File confirmed successfully.";
            }
//            var f = new JobListforDLPM();
  //          f.JobList = dal.GetListOfJobsForDLPM(WebSecurity.CurrentUserId);
            return RedirectToAction("Conformation");
//            return View(f);
        }

        public ActionResult AddNotes(int JobNo)
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddNotes(int JobNo, string Notes)
        {
            dal.AddUpdateJobNotes(JobNo, Notes);
            return RedirectToAction("Conformation");
        }
    
    }
}
