using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDYY_WEB.Models
{
    public class MenuModels
    {
        public string ItemID { get; set; }
        public string ItemValue { get; set; }

        //public static List<MenuModels> TumortageMenu = new List<MenuModels>()
        //{ 
        //    new  { "1", "IIIB期" }, 
        //    { "2", "T2N0M1" }, 
        //    { "3", "T1bN3M1b" },
        //    {"4", "IIIB期"}
        //};

        public static List<MenuModels> TumortageMenu = new List<MenuModels>() { 
            new MenuModels() { ItemID = "1", ItemValue = "IIIB" }, 
            new MenuModels() { ItemID = "2", ItemValue = "T2N0M1" },
            new MenuModels() { ItemID = "3", ItemValue = "T1bN3M1b" },
            new MenuModels() { ItemID = "4", ItemValue = "IIIB" },
        };

    }

    
}
