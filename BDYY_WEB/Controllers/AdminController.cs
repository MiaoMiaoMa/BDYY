using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using BDYY_WEB.Classes;
using BDYY_WEB.Models;
using DataProvider;
using System.IO;

namespace BDYY_WEB.Controllers
{
    public class AdminController : BaseController
    {
        AdminProvider db = new AdminProvider();
        UploadFilesProvider fileDB = new UploadFilesProvider();

        #region View

        public ActionResult Login()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult Default()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult AppointmentApprove()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult ApproveManage(string uid)
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult ApproveManageList()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult AppointmentQueryList()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult ServiceManage()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult DrugStoreList()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult Doctor()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult hospital()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult geography()
        {
            return View();
        }

        #endregion


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Admin");
        }

        //用户登录验证
        public JsonResult AdminLoginValidate(string uid, string pwd)
        {
            bool IsSuccess = false;
            try
            {
                AdminModels admin = db.CheckUserLogin(uid, pwd);
                if (!string.IsNullOrEmpty(admin.UserID))
                {
                    IsSuccess = true;
                    Session[USRID] = admin.UserID;
                    Session[ATTACHMENTDIR] = AttachmentDir();
                    FormsAuthentication.SetAuthCookie(string.Format("{0}", admin.UserID), false);
                }
            }
            catch { }

            return Json(new { IsSuccess = IsSuccess }, "text/html", JsonRequestBehavior.AllowGet);
        }

        private string AttachmentDir()
        {
            string attachmentDir = this.Request.PhysicalApplicationPath + "Upload\\";
            if (!Directory.Exists(attachmentDir))
            {
                Directory.CreateDirectory(attachmentDir);
            }
            return attachmentDir;
        }

        #region 预约审核
        //获取预约审核列表
        public string GetAppointReviewList(string searchType, string reviewType, string searchContentStart, string searchContentEnd)
        {
            List<UsersModel> patientList = db.GetAppointmentReview(searchType, reviewType, searchContentStart, searchContentEnd);

            return JsonConvert.SerializeObject(patientList);
        }

        [SessionExpireFilter]
        public JsonResult Approve(string petientID)
        {
            bool result = db.ReviewBaseInfor(petientID, Session[USRID].ToString());
            return Json(new { result = result }, "text/html", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 赠药审批管理

        /// <summary>
        /// “赠药审批管理”，“客服管理”返回界面和预约审核界面一样。审核部门ID是1. 客服部门的部门ID是2.
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="reviewType"></param>
        /// <param name="department"></param>
        /// <param name="searchContentStart"></param>
        /// <param name="searchContentEnd"></param>
        /// <returns></returns>
        public string GetApplicationReviewList(string searchType, string reviewType, string department, string searchContentStart, string searchContentEnd)
        {
            List<UsersModel> patientList = db.GetApplicationReview(searchType, reviewType, department, searchContentStart, searchContentEnd);

            return JsonConvert.SerializeObject(patientList);
        }

        //审批页面获取用户全部信息
        [SessionExpireFilter]
        public string GetPatinetALlInfo(string uid)
        {
            UsersModel patient = new UsersModel();
            UsersProvider udb = new UsersProvider();
            patient = udb.GetPatientInfor(uid);
            CommentModels comment = new CommentModels();
            comment.CommentPatientID = uid;
            comment.CommentOperater = Session[USRID].ToString();
            List<CommentModels> commentsList = db.GetCommentsList(uid);
            List<UploadFileModels> fileList = fileDB.GetUploadFilesByPatientID(uid);
            
            var result = new
            {
                patient = patient,
                comments = comment,
                commentsList = commentsList,
                filesList = fileList
            };
            return JsonConvert.SerializeObject(result);
        }

        //更新部门或者状态
        [SessionExpireFilter]
        public string UpdateDepartOrStatus(string patientID, string departTo, string statusTo, string isAppointment)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(patientID))
            {
                result = db.UpdatePatientDepartOrStatus(patientID, Session[USRID].ToString(), departTo, statusTo, isAppointment);
            }

            var data = new
            {
                result = result
            };
            return JsonConvert.SerializeObject(data);
        }

        [SessionExpireFilter]
        public string AddComment(string data, string patientID)
        {
            bool result = false;
            List<CommentModels> commentsList = null;
            if (!string.IsNullOrEmpty(data))
            {
                //var comment = JsonConvert.DeserializeObject<CommentModels>(data);
                CommentModels comment = new CommentModels()
                {
                    CommentOperater = Session[USRID].ToString(),
                    CommentPatientID = patientID,
                    CommentContent = Server.HtmlEncode(data)
                };
                result = db.AddComments(comment);
                commentsList = db.GetCommentsList(patientID);
            }
            return JsonConvert.SerializeObject(commentsList);
        }

        [HttpPost]
        [SessionExpireFilter]
        public string AddFile(FormCollection formValues)
        {            
            string patientID = formValues["patientID"].ToString();
            //deal with attachment
            HttpFileCollectionBase files = HttpContext.Request.Files;
            List<UploadFileModels> attachmentlist = new List<UploadFileModels>();
            UploadFiles uploadFiles = new UploadFiles();
            string fileExt = string.Empty;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase hpfb = files[i];
                Guid fileID;
                if (hpfb.ContentLength > 0)
                {
                   uploadFiles.SaveUploadFiles(hpfb.InputStream, hpfb.FileName, out fileExt, out fileID, patientID);                    
                }
            }

            attachmentlist = fileDB.GetUploadFilesByPatientID(patientID);   
            return JsonConvert.SerializeObject(attachmentlist);

        }
        #endregion

    }
}
