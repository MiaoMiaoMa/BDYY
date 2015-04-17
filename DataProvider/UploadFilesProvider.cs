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
    public class UploadFilesProvider    : ProviderBase
    {
        //创建附件
        public bool createFile(Guid FileID, string filename, string patientUID)
        {

            SqlParameter[] parameters = {
                                            new SqlParameter("AttachedFileName", filename),
                                            new SqlParameter("Patient_Usr_ID", patientUID),
                                            new SqlParameter("UniquelyIdentified", FileID)
                                        };

            if (SQLHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "AddAttachedFile", parameters) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //获取附件列表
        public List<UploadFileModels> GetUploadFilesByPatientID(string patientID)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("Patient_Usr_ID", patientID)
                                        };
            return SQLHelper.GetModelList<UploadFileModels>(getModelList, ConnectionString, "GetAttachedFileList", parameters);
        }

        //map to 附件
        private List<UploadFileModels> getModelList(SqlDataReader reader)
        { 
            List<UploadFileModels> fileList = new List<UploadFileModels>();

            while (reader.Read())
            {
                UploadFileModels file = new UploadFileModels();
                file.FileName = GetReaderToString(reader["AttachedFileName"]);
                file.FileAddDate = GetReaderToDateTimeString(reader["InputDate"]);
                file.PatientID = GetReaderToString(reader["Patient_Usr_ID"]);
                file.FileID = GetReaderToString(reader["AttachedID"]);
                file.IsFirstApplication = GetReaderToBool(reader["IsFirstApplication"]);
                file.FileGUID = reader.GetGuid(5);
                file.FileGUIDName = file.FileGUID.ToString() + System.IO.Path.GetExtension(file.FileName);
                
                fileList.Add(file);
            }

            return fileList;
        }
    }
}
