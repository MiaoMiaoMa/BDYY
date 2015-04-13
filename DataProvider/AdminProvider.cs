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

        private AdminModels getUserModel(SqlDataReader reader)
        {
            AdminModels admin = new AdminModels();
            admin.UserID = GetReaderToString(reader["Usr_Name"]);
            admin.UserDepart = GetReaderToString(reader["Usr_Desc"]);
            return admin;
        }
    }
}
