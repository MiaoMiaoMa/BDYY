using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using BDYY_WEB.Classes;
using BDYY_WEB.Models;
using DataProvider;


namespace BDYY_WEB.Controllers
{
    public class AccountController : BaseController
    {
        #region Views
        [SessionExpireFilter]
        public ActionResult AccountInfo()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult ChangePWD()
        {
            return View();
        }
        [SessionExpireFilter]
        public ActionResult ApplyFor()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult ApplyAgain()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult StatusCheck()
        {
            return View();
        }
        #endregion

        #region function
        public JsonResult UpdatePWD(string newpwd)
        {
            bool IsSuccess = true;

            return Json(new { IsSuccess = IsSuccess }, "text/html", JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public string GetPatientAllInfo(string uid)
        {
            UsersModel Patient = new UsersModel();
            UsersProvider db = new UsersProvider();
            if (string.IsNullOrEmpty(uid))
            {
                uid = Session[USRID].ToString();
            }
            Patient = db.GetPatientInfor(uid);
            return JsonConvert.SerializeObject(Patient);            
        }

        public string GetApplyFor()
        { 
            ApplyProvider db = new ApplyProvider();
            UsersProvider udb = new UsersProvider();
            UsersModel patientOtherInfo = new UsersModel();
            patientOtherInfo = udb.GetPatientInfor(Session[USRID].ToString());            
            patientOtherInfo.UserID = Session[USRID].ToString();
            patientOtherInfo.IsGeneDetect = string.IsNullOrEmpty(patientOtherInfo.IsGeneDetect) ? "1" : patientOtherInfo.IsGeneDetect;
            patientOtherInfo.Tumortage = string.IsNullOrEmpty(patientOtherInfo.Tumortage) ? "1" : patientOtherInfo.Tumortage;
            patientOtherInfo.IsDiseaseDiagnosis = string.IsNullOrEmpty(patientOtherInfo.IsDiseaseDiagnosis) ? "1" : patientOtherInfo.IsDiseaseDiagnosis;
            patientOtherInfo.IsAccordFiveMouth = string.IsNullOrEmpty(patientOtherInfo.IsAccordFiveMouth) ? "1" : patientOtherInfo.IsAccordFiveMouth;
            //检查是否预约通过审核
            string patientStatus = db.CheckPatientStatus(Session[USRID].ToString());
            var data = new
            {
                patientStatus = patientStatus,
                patientOtherInfo = patientOtherInfo
            };
            return JsonConvert.SerializeObject(data);
        }

        //首次提交
        public JsonResult AddApplyFor(string data)
        {
            bool result = false;
            string errormsg = string.Empty;
            if (!string.IsNullOrEmpty(data))
            {
                UsersModel patient = JsonConvert.DeserializeObject<UsersModel>(data);
                ApplyProvider db = new ApplyProvider();
                //首次申请用药提交前检查是否已经提交过。
                if (!db.CheckApplyIsExist(Session[USRID].ToString()))
                {
                    result = db.UpdatePatientOtherInfo(patient);
                }
                else
                {
                    errormsg = "您已经提交过首次申请用药！";
                }
            }

            return Json(new { IsSuccess = result, errormsg = errormsg}, "text/html", JsonRequestBehavior.AllowGet);            
        }

        public JsonResult AddApplyForAgain(string emsNumber)
        {
            string result = string.Empty;
            string msg = string.Empty;
            bool isSuccess = false;
            if (!string.IsNullOrEmpty(emsNumber))
            {
                ApplyProvider db = new ApplyProvider();
                //检查是否可以提交续领申请用药（即EMS号）1是可以提交。0 是不可以提交
                db.CheckApplyIsSubmit(Session[USRID].ToString(), emsNumber, out result, out msg);
                if (result == "1")
                {
                    isSuccess = db.UpdatePatientAgainInfo(Session[USRID].ToString(), emsNumber);
                }
            }

            return Json(new { IsSuccess = isSuccess, errormsg = msg }, "text/html", JsonRequestBehavior.AllowGet);            
        }

        public string GetStatusCheck(string patientID)
        {
            return string.Empty;
        }

        #endregion

    }
}
