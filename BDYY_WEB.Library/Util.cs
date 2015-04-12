using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDYY_WEB.Library
{
    public class Util
    {
        /// <summary>
        /// return "1900-02-28"
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static string ConvertToDateTimeStr(string dateStr)
        {
            DateTime date;
            if(DateTime.TryParse(dateStr, out date))
            {
                return date.ToString("yyyy-MM-dd");
            }
            else
            {
                return string.Empty;
            }            
        }
    }
}
