using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Bergeno_lituanistine_mokiklele.Models {
    public class UserBasicInfo {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public string RoleText { get; set; }
    }

    public enum RoleDetails {
        Narys = 1,
        Redaktorius,
        Administratorius,
    }
}