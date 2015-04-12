using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BDYY_WEB.Models;
using System.Data;

namespace DataProvider
{
    public class UsersProvider : ProviderBase
    {
        //用户登入
        public UsersModel CheckUserLogin(string uid, string pwd)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("Usr_Name", SqlDbType.VarChar),
                                            new SqlParameter("Usr_Password", SqlDbType.VarChar)
                                        };
            parameters[0].Value = uid;
            parameters[1].Value = pwd;

            return SQLHelper.GetModel(getUserModel, ConnectionString, "[PatientUserLogin]", parameters);            
        }

        //修改密码
        public bool UpdatePWD(string uid, string pwd)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("Usr_Name", SqlDbType.VarChar),
                                            new SqlParameter("Usr_Password", SqlDbType.VarChar)
                                        };
            parameters[0].Value = uid;
            parameters[1].Value = pwd;

            if (SQLHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "", parameters) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //提交预约前检查身份证是否存在 true 存在， false不存在
        public bool CheckUserIsExist(string IdentityNumber)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("Identity_number", SqlDbType.VarChar)
                                        };
            parameters[0].Value = IdentityNumber;

            SqlDataReader reader = SQLHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "CheckUserExist", parameters);
            if(reader.Read() )
            {
                if (reader["Patient_Usr_ID"] == DBNull.Value)
                {
                    reader.Close();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
            return false;                        
        }

        //预约
        public void AddPatientInfo(UsersModel patient, out string identityNumber, out string pwd)
        {
            identityNumber = string.Empty;
            pwd = string.Empty;
            SqlParameter[] parameters = {
                                            new SqlParameter("Patient_Name", patient.UserName),
                                            new SqlParameter("Gender", patient.Gender),
                                            new SqlParameter("Birthday", patient.Birthday),
                                            new SqlParameter("Mobil_PhoneNumber", patient.MobilPhoneNumber),
                                            new SqlParameter("Identity_Type", patient.IdentityType),
                                            new SqlParameter("Identity_number", patient.IdentityNumber),
                                            new SqlParameter("Emg_PhoneNumber", patient.EmgPhoneNumber),
                                            new SqlParameter("Mailbox", patient.Mailbox),
                                            new SqlParameter("Province_Name", patient.Province),
                                            new SqlParameter("City_Name", patient.City),
                                            new SqlParameter("Permanent_Address", patient.Address),
                                            new SqlParameter("lastModify_Date", patient.LastModifyDate),
                                            new SqlParameter("Hospital_Name", patient.Hospital),
                                            new SqlParameter("Doctor", patient.Doctor),
                                            new SqlParameter("Smoking_history_Type", patient.SmokingHisType),
                                            new SqlParameter("Smoking_history", patient.SmokingHis),
                                            new SqlParameter("Pathology_Type", patient.PathologyType),
                                            new SqlParameter("First_Use_Date", patient.FirstUseDate)
                                        };

            SqlDataReader reader = SQLHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "Add_Patient_BaseInfor", parameters);
            if (reader.Read())
            {
                identityNumber = reader.GetString(0);
                pwd = reader.GetString(3);
                reader.Close();
            }            
        }        

        //获取用户基本信息
        public UsersModel GetPatientInfor(string uid)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("Patient_Usr_ID", SqlDbType.VarChar)
                                        };
            parameters[0].Value = uid;

            return SQLHelper.GetModel(getUserModelInfoAll, ConnectionString, "[GetPatientInfor]", parameters);
        }

        //Map to 用户登入信息
        private UsersModel getUserModel(SqlDataReader reader)
        {
            UsersModel user = new UsersModel();
           
            user.UserID = reader["Patient_Usr_ID"].ToString();
            user.UserName = reader["Patient_Name"].ToString();
            user.Isverify = reader["Isverify"].ToString() == "0" ? false : true;
            user.FirstUseDate = GetReaderToDateTimeString(reader["First_Use_Date"]);//reader["First_Use_Date"] == DBNull.Value ? "" : Convert.ToDateTime(reader["First_Use_Date"].ToString()).ToString("yyyy-MM-dd");
          
            return user;
        }

        //Map to 用户基本信息
        private UsersModel getUserModelInfoAll(SqlDataReader reader)
        {
            UsersModel user = new UsersModel();

            user.UserID = reader["Patient_Usr_ID"].ToString();
            user.UserName = reader["Patient_Name"].ToString();
            user.Isverify = reader["Isverify"].ToString() == "0" ? false : true;
            user.FirstUseDate = GetReaderToDateTimeString(reader["First_Use_Date"]);//reader["First_Use_Date"] == DBNull.Value ? "" : Convert.ToDateTime(reader["First_Use_Date"].ToString()).ToString("yyyy-MM-dd");
            user.Gender = GetReaderToString(reader["Gender"]);
            user.Birthday = GetReaderToDateTimeString(reader["Birthday"]); //reader["Birthday"] == DBNull.Value ? "" : Convert.ToDateTime(reader["Birthday"].ToString()).ToString("yyyy-MM-dd");
            user.MobilPhoneNumber = GetReaderToString(reader["Mobil_PhoneNumber"]);//reader["Mobil_PhoneNumber"] == DBNull.Value ? "" : reader["Mobil_PhoneNumber"].ToString();
            user.EmgPhoneNumber = GetReaderToString(reader["Emg_PhoneNumber"]);//reader["Emg_PhoneNumber"] == DBNull.Value ? "" : reader["Emg_PhoneNumber"].ToString();
            user.IdentityNumber = GetReaderToString(reader["Identity_number"]);
            user.Mailbox = GetReaderToString(reader["Mailbox"]);
            user.Province = GetReaderToString(reader["Province_Name"]);
            user.City = GetReaderToString(reader["City_Name"]);
            user.Address = GetReaderToString(reader["Permanent_Address"]);
            user.RegistrationDate = GetReaderToDateTimeString(reader["Registration_Date"]);
            user.Hospital = GetReaderToString(reader["Hospital_Name"]);
            user.Doctor = GetReaderToString(reader["Doctor"]);
            user.SmokingHisType = GetReaderToString(reader["Smoking_history_Type"]);
            user.SmokingHis = GetReaderToString(reader["Smoking_history"]);
            
            
            return user;
        }
    }
}
