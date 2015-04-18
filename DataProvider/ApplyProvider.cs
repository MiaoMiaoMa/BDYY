using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDYY_WEB.Models;

namespace DataProvider
{
    public class ApplyProvider : ProviderBase
    {
        /// <summary>
        /// 首次申请用药前，需要检查预约是否通过审核，没通过不可以提交
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>是否预约通过审核   0待审核，2未通过，3审核通过</returns>
        public string CheckPatientStatus(string uid)
        {
            string status = string.Empty;
            SqlParameter[] parameters = {
                                            new SqlParameter("Patient_Usr_ID", SqlDbType.VarChar)
                                        };
            parameters[0].Value = uid;

            SqlDataReader reader = SQLHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "IsExamineOfAppointment", parameters);
            if (reader.Read())
            {
                if (reader["Isverify"] != DBNull.Value)
                {
                    status = reader["Isverify"].ToString();
                    reader.Close();
                }
            }

            return status;
        }

        //首次申请用药提交前检查是否已经提交过。有返回记录说明提交过，否则没提交过
        public bool CheckApplyIsExist(string uid)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("Patient_Usr_ID", SqlDbType.VarChar)
                                        };
            parameters[0].Value = uid;

            SqlDataReader reader = SQLHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "IsExitFirstApplication", parameters);
            if (reader.Read())
            {
                if (reader["Patient_Usr_ID"] == DBNull.Value)
                {
                    reader.Close();
                    return false;
                }
                else
                {
                    reader.Close();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 检查是否可以提交续领申请用药,(EMS号）1是可以提交。0 是不可以提交
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="emsNumber"></param>
        /// <returns></returns>
        public void CheckApplyIsSubmit(string uid, string emsNumber, out string result, out string errormsg)
        {
            result = string.Empty;
            errormsg = string.Empty;

            SqlParameter[] parameters = {
                                            new SqlParameter("Patient_Usr_ID", SqlDbType.VarChar),
                                            new SqlParameter("EMS_Number", SqlDbType.VarChar)
                                        };
            parameters[0].Value = uid;
            parameters[1].Value = emsNumber;

            SqlDataReader reader = SQLHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "IsCanSubmitContApply", parameters);
            if (reader.Read())
            {
                result = GetReaderToString(reader["ContApplyResult"]);
                errormsg = GetReaderToString(reader["Message"]);
                reader.Close();
            }
        }

        //首次用药申请
        public bool UpdatePatientOtherInfo(UsersModel patient)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("Patient_Usr_ID", patient.UserID),
                                            new SqlParameter("Expected_Lead_Date", patient.ExpectedLeadDate),
                                            new SqlParameter("KPS_Score", patient.KPSScore == null ? "0" : patient.KPSScore),
                                            new SqlParameter("Tumor_tage", patient.Tumortage),
                                            new SqlParameter("IsGeneDetect", patient.IsGeneDetect),
                                            new SqlParameter("Required_Reply_Date", patient.RequiredReplyDate == null ? "" : patient.RequiredReplyDate),
                                            new SqlParameter("EMS_Number", patient.EMSNumber == null ? "" : patient.EMSNumber),
                                            new SqlParameter("Seller_Name", patient.SellerName == null ? "" : patient.SellerName),
                                            new SqlParameter("IsAccordFiveMouth", patient.IsAccordFiveMouth),
                                            new SqlParameter("IsDiseaseDiagnosis", patient.IsDiseaseDiagnosis),
                                        };
            if (SQLHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "Update_Patient_OtherInfor", parameters) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //继续用药申请
        public bool UpdatePatientAgainInfo(string uid, string emsNumber)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("Patient_Usr_ID", uid),                                        
                                            new SqlParameter("EMS_Number", emsNumber)
                                        };
            if (SQLHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "FirstApplyLeadMedicine", parameters) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        /// <summary>
        /// 待完成
        /// </summary>
        /// <param name="uid"></param>
        public void GetReviewStatus(string uid)
        {
            string status = string.Empty;
            SqlParameter[] parameters = {
                                            new SqlParameter("Patient_Usr_ID", SqlDbType.VarChar)
                                        };
            parameters[0].Value = uid;

            SqlDataReader reader = SQLHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "GetReviewStatus", parameters);
            if (reader.Read())
            {
                
            }
        }

    }
}
