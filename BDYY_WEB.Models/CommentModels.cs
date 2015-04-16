using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDYY_WEB.Models
{
    public class CommentModels
    {
        public string CommentID { get; set; }
        public string CommentPatientID { get; set; }
        public string CommentOperater { get; set; }
        public string CommentDate { get; set; }
        public string CommentContent { get; set; }
    }
}
