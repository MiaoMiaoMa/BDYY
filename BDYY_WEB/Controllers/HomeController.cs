using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using BDYY_WEB.Models;
using DataProvider;

namespace BDYY_WEB.Controllers
{
    public class HomeController : BaseController
    {
        #region View
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddInfo()
        {
            return View();
        }

        //预约成功
        public ActionResult Success()
        {         
            if (Session["IdentityNumber"] != null)
            {
                ViewBag.IdentityNumber = Session["IdentityNumber"].ToString();
                ViewBag.pwd = Session["IdentityPWD"].ToString();
                Session["IdentityNumber"] = null;
                Session["IdentityPWD"] = null;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        //注销
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        //用户登录验证
        public JsonResult LoginValidate(string uid, string pwd)
        {
            bool IsSuccess = false;
            try
            {
                UsersModel user = new UsersModel();
                UsersProvider userdb = new UsersProvider();
                user = userdb.CheckUserLogin(uid, pwd);
                if (!string.IsNullOrEmpty(user.UserID))
                {
                    IsSuccess = true;
                    Session[USRID] = user.UserID;
                    FormsAuthentication.SetAuthCookie(string.Format("{0},{1},{2}", user.UserName, "",""), false);
                }
            }
            catch { }

            return Json(new { IsSuccess = IsSuccess }, "text/html", JsonRequestBehavior.AllowGet);
        }

        public string GetPatientInfoEmptyModel()
        {
            UsersModel patient = new UsersModel();
            patient.IdentityType = "1";
            patient.Gender = "男";
            patient.Hospital = "杭州师范大学附属医院";
            patient.PathologyType = "鳞癌";
            //patient.Province = "请选择";
            //patient.City = "请选择";
            
            var data = new
            {
                patient = patient
            };
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// 添加预约登记
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult AddPatientInfo(string data)
        {
            bool isSuccess = false;
            string errormsg = "";
            string identityNumber = string.Empty;
            string pwd = string.Empty;
            try
            {
                if (data != null)
                {
                    UsersModel patient = JsonConvert.DeserializeObject<UsersModel>(data);
                    UsersProvider db = new UsersProvider();
                    if (!db.CheckUserIsExist(patient.IdentityNumber))
                    {
                        patient.IdentityType = string.IsNullOrEmpty(patient.IdentityType) ? "1" : patient.IdentityType;
                        patient.Gender = string.IsNullOrEmpty(patient.Gender) ? "男" : patient.Gender;
                        patient.SmokingHisType = string.IsNullOrEmpty(patient.SmokingHisType) ? "1" : patient.SmokingHisType;

                        db.AddPatientInfo(patient, out identityNumber, out pwd);

                        if (!string.IsNullOrEmpty(identityNumber))
                        {
                            isSuccess = true;
                            Session["IdentityNumber"] = identityNumber;
                            Session["IdentityPWD"] = pwd;
                        }
                    }
                    else
                    {
                        errormsg = "您的证件号码已经被使用！";
                    }
                }
            }
            catch { }

            return Json(new { IsSuccess = isSuccess, errormsg = errormsg }, "text/html", JsonRequestBehavior.AllowGet);
        }


    }
}
