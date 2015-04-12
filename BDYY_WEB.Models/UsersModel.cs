using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDYY_WEB.Models
{
    public class UsersModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserPWD { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string MobilPhoneNumber { get; set; }
        public string EmgPhoneNumber { get; set; }
        public string IdentityType { get; set; }
        public string IdentityNumber { get; set; }
        public string Mailbox { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string RegistrationDate { get; set; }
        public string LastModifyDate { get; set; }
        public string Hospital { get; set; }
        public string Doctor { get; set; }
        public string City { get; set; }
        public string SmokingHisType { get; set; }
        public string SmokingHis { get; set; }
        public string PathologyType { get; set; }
        public bool Isverify { get; set; }
        public string FirstUseDate { get; set; }

        public string ExpectedLeadDate { get; set; }
        public string KPSScore { get; set; }
        public string Tumortage { get; set; }
        public string IsGeneDetect { get; set; }
        public string RequiredReplyDate { get; set; }
        public string EMSNumber { get; set; }
        public string SellerName { get; set; }
        public string IsAccordFiveMouth { get; set; }
        public string IsDiseaseDiagnosis { get; set; }
    }
}
