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

namespace BDYY_WEB.Controllers
{
    public class AdminController : BaseController
    {
        AdminProvider db = new AdminProvider();

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
        public ActionResult ApproveManage()
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
                    FormsAuthentication.SetAuthCookie(string.Format("{0}", admin.UserID), false);
                }
            }
            catch { }

            return Json(new { IsSuccess = IsSuccess }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //获取预约审核列表
        public string GetAppointReviewList(string searchType, string reviewType, string searchContentStart, string searchContentEnd)
        {
            List<UsersModel> patientList = db.GetAppointmentReview(searchType, reviewType, searchContentStart, searchContentEnd);

            return JsonConvert.SerializeObject(patientList);
        }

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

    }
}
