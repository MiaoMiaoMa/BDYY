using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{
    public class ProviderBase
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;

        public static string GetReaderToString(object reader)
        {
            if (reader == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                return reader.ToString();
            }
        }

        /// <summary>
        /// return "1900-02-28"
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static string GetReaderToDateTimeString(object reader)
        {
            DateTime date;
            if (reader == DBNull.Value)
            {
                return string.Empty;
            }
            else
            { 
                if(DateTime.TryParse(reader.ToString(), out date))
                {
                    return date.ToString("yyyy-MM-dd");
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static int CalculateAgeByBirthDay(string birthDay)
        {
            DateTime now = DateTime.Now;
            DateTime birthDate;
            int age = 0;
            if (DateTime.TryParse(birthDay, out birthDate))
            {
                age = now.Year - birthDate.Year;
                if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            }
            return age;
        }
    }
}
