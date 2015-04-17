using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDYY_WEB.Models
{
    public class UploadFileModels
    {
        public string FileID { get; set; }
        public Guid FileGUID { get; set; }
        public string FileName { get; set; }
        public string FileAddDate { get; set; }
        public string PatientID { get; set; }
        public bool IsFirstApplication { get; set; }
        public string FileGUIDName { get; set; }
    }
}
