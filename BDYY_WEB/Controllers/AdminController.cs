using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BDYY_WEB.Classes;
using BDYY_WEB.Models;
using DataProvider;

namespace BDYY_WEB.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

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
                AdminProvider db = new AdminProvider();
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

    }
}
