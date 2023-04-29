using BD_Bergeno_lituanistine_mokiklele.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Bergeno_lituanistine_mokiklele.Services {
    public class RegisterService : IRegisterService {
        

        public async Task<RegisterResponse> RegisterNewUser(RegisterRequest request) {
            
            using (var client = new HttpClient()) {

                try {
                    //TODO update simple JWT way
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
                        return JsonConvert.DeserializeObject<RegisterResponse>(json);
                    }
                    
                }
                catch (Exception ex) {
                    Debug.WriteLine($"Whoops exception: {ex.Message}");
                    var registrationFailure = Toast.Make("Whoops something is wrong", ToastDuration.Long);
                    await registrationFailure.Show();
                    return null;
                }
            }
        }        
    }
}
