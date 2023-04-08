
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Bergeno_lituanistine_mokiklele.Models {
    public class LoginResponse {
        public string Token { get; set; }   
        public string User_email { get; set; }
        public string User_nicename { get; set; }
        public string User_display_name { get; set; }

        //sito nereikes turbut dabar
        public UserBasicInfo UserBasicInfo = new UserBasicInfo();

    }
}
