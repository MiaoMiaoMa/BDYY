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
    public class AdminProvider : ProviderBase
    {
        //用户登入
        public AdminModels CheckUserLogin(string uid, string pwd)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("Usr_Name", SqlDbType.VarChar),
                                            new SqlParameter("Usr_Password", SqlDbType.VarChar)
                                        };
            parameters[0].Value = uid;
            parameters[1].Value = pwd;

            return SQLHelper.GetModel(getUserModel, ConnectionString, "[CheckUser]", parameters);
        }

        /// <summary>
        /// 获取预约审核列表
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="reviewType"></param>
        /// <param name="searchContentStart"></param>
        /// <param name="searchContentEnd"></param>
        /// <returns></returns>
        public List<UsersModel> GetAppointmentReview(string searchType, string reviewType, string searchContentStart, string searchContentEnd)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("SearchType", searchType),
                                            new SqlParameter("ReviewType", reviewType),
                                            new SqlParameter("SearchContentStart", searchContentStart),
                                            new SqlParameter("SearchContentEnd", searchContentEnd)
                                         };
            return SQLHelper.GetModelList<UsersModel>(UsersProvider.getUserModelInfoList, ConnectionString, "GetAppointmentReview", parameters);
        }

        /// <summary>
        /// 赠药审批管理”，“客服管理”返回界面和预约审核界面一样。审核部门ID是1. 客服部门的部门ID是2.
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="reviewType"></param>
        /// <param name="department"></param>
        /// <param name="searchContentStart"></param>
        /// <param name="searchContentEnd"></param>
        /// <returns></returns>
        public List<UsersModel> GetApplicationReview(string searchType, string reviewType, string department, string searchContentStart, string searchContentEnd)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("SearchType", searchType),
                                            new SqlParameter("ReviewType", reviewType),
                                            new SqlParameter("Department", department),
                                            new SqlParameter("SearchContentStart", searchContentStart),
                                            new SqlParameter("SearchContentEnd", searchContentEnd)
                                         };
            return SQLHelper.GetModelList<UsersModel>(UsersProvider.getUserModelInfoList, ConnectionString, "GetApplicationReview", parameters);
        }

        //修改密码
        public bool ReviewBaseInfor(string patientId, string operater)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("Patient_Usr_ID", SqlDbType.VarChar),
                                            new SqlParameter("Operater", SqlDbType.VarChar)
                                        };
            parameters[0].Value = patientId;
            parameters[1].Value = operater;

            if (SQLHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "ReviewBaseInfor", parameters) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Map to Admin Entity
        private AdminModels getUserModel(SqlDataReader reader)
        {
            AdminModels admin = new AdminModels();
            admin.UserID = GetReaderToString(reader["Usr_Name"]);
            admin.UserDepart = GetReaderToString(reader["Usr_Desc"]);
            return admin;
        }

    }
}
