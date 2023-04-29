using BD_Bergeno_lituanistine_mokiklele.Platforms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Bergeno_lituanistine_mokiklele.Models {
    public class UserBasicInfo {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public string Role { get; set; }
        public string Cookies { get; set; }
        public string FullName 
            {
            get => $"{FirstName} {LastName}";
            }

        //for api response
        public string Token { get; set; }
        public string User_email { get; set; }
        public string User_nicename { get; set;}
        public string user_display_name { get; set;}
    }

    public enum RoleDetails {             
        Administrator = 1,
        Author,
        Subscriber 
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class AvatarUrls {
        [JsonProperty("24")]
        public string _24 { get; set; }

        [JsonProperty("48")]
        public string _48 { get; set; }

        [JsonProperty("96")]
        public string _96 { get; set; }
    }

    public class Collection {
        public string href { get; set; }
    }

    public class Links {
        public List<Self> self { get; set; }
        public List<Collection> collection { get; set; }
    }

    public class Root {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string slug { get; set; }
        public AvatarUrls avatar_urls { get; set; }
        public List<object> meta { get; set; }
        public List<object> acf { get; set; }
        public List<string> roles { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public Links _links { get; set; }
    }

    public class Self {
        public string href { get; set; }
    }


}