using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;
using Rebex.Net;
using WebMatrix.WebData;
using System.Configuration;
using System.IO;

namespace LCLFileUpload.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!WebSecurity.Initialized)
            {
                try
                {
                    WebSecurity.InitializeDatabaseConnection(ConfigurationManager.ConnectionStrings["WEBWISDOMConnection"].ConnectionString, "System.Data.SqlClient", "tblUserInfo", "LoginID", "UserName", autoCreateTables: true);
                }
                catch { }
            }
            ViewBag.Message = "Click on Choose Files to select the Terminal Files to upload.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public FileResult RemoteprocessingLoginInstructions()
        {
            string filepath = Server.MapPath("/Views//Home//RemoteprocessingLoginInstructions.pdf");
            byte[] pdfByte = GetBytesFromFile(filepath);
            return File(pdfByte, "application/pdf");
        }

        public FileResult RemoteprocessingInstructions()
        {
            string filepath = Server.MapPath("/Views//Home//RemoteprocessingProcessingInstructions.pdf");
            byte[] pdfByte = GetBytesFromFile(filepath);
            return File(pdfByte, "application/pdf");
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase[] files)
        {
            try
            {
                Sftp client = new Sftp();
                
                string hostname= System.Configuration.ConfigurationManager.AppSettings["hostname"];
                string userName = System.Configuration.ConfigurationManager.AppSettings["username"].ToString();
                string password = System.Configuration.ConfigurationManager.AppSettings["password"].ToString();
                client.Connect(hostname);
                client.Login(userName, password);
                client.ChangeDirectory("inbox");
                
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
            return View(); 
            //return RedirectToAction("Index");
        }

        public static byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)

            FileStream fs = null;
            try
            {
                fs = System.IO.File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        public ActionResult UploadToAmazon(DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
//            AmazonConnectionInfo connInfo = UploadControlDemosHelper.GetAmazonConnectionInfo();
  //          return DemoView("UploadToAmazon", connInfo);
            return null;
        }

        [HttpPost]
        public ActionResult ProcessFile(object sender)
//        public ActionResult ProcessFile(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            return null;
            //            using (System.IO.Stream stream = e.UploadedFile.FileContent)
  //          {
                // do something with stream 
    //        }

//            ViewBag.Message = "Your contact page.";

  //          return View();
        }
    }
}
