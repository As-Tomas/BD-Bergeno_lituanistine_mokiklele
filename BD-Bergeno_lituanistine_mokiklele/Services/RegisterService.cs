using BD_Bergeno_lituanistine_mokiklele.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Bergeno_lituanistine_mokiklele.Services {
    public class RegisterService : IRegisterService {
        public async Task<RegisterResponse> RegisterNewUser(RegisterRequest request) {
            
            using (var client = new HttpClient()) {

                try {

                    string registerRequestStr = JsonConvert.SerializeObject(request);
                    var content = new StringContent(registerRequestStr, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("https://www.webbiter.com/index.php/wp-json/wp/v2/users/register", 
                        content);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                        
                        var json = await response.Content.ReadAsStringAsync();

                      
                        return JsonConvert.DeserializeObject<RegisterResponse>(json);
                    }
                    else {

                        var json = await response.Content.ReadAsStringAsync();
                        var kascia = JsonConvert.DeserializeObject<RegisterResponse>(json);

                        Int32 responseCode = (Int32)response.StatusCode;

                        if (responseCode.Equals(400)) {
                            await AppShell.Current.DisplayAlert("Vartotojo vardas netinkamas!", "Username field 'username' is required.", "OK");
                        }
                        else {
                            await AppShell.Current.DisplayAlert("Invalid registration!", "Sorry something vent wrong", "OK");
                        }
                        return null;
                    }
                }
                catch (Exception ex) {
                    //Debug.WriteLine($"Whoops exception: {ex.Message}");
                    return null;
                }
            }
        }

        public async Task<UserBasicInfo> UpdateUserBasicInfo(RegisterRequest request) {

            using (var client = new HttpClient()) {

                //string registerRequestStr = JsonConvert.SerializeObject(request);

                string requestUserByEmail = request.email;

                var response = await client.GetAsync($"https://www.webbiter.com/index.php/wp-json/wp/v2/users?search={requestUserByEmail}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UserBasicInfo>(json);
                }
                else {
                    return null;
                }
            }
        }
    }
}
