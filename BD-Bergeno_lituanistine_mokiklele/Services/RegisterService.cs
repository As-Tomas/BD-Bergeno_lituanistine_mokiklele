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
                    registrationFailure.Show();
                    return null;
                }
            }
        }


        //public async Task<LoginResponse> Authenticate(LoginRequest loginRequest) {
        //    using (var _httpClient = new HttpClient()) {


        //        try {
        //            string loginRequestStr = JsonConvert.SerializeObject(loginRequest);
        //            HttpResponseMessage response = await _httpClient.PostAsync("https://www.webbiter.com/index.php/wp-json/jwt-auth/v1/token",
        //                new StringContent(loginRequestStr, Encoding.UTF8, "application/json"));

        //            if (response.StatusCode == System.Net.HttpStatusCode.OK) {

        //                var json = await response.Content.ReadAsStringAsync();
        //                var responseObject = JsonConvert.DeserializeObject<LoginResponse>(json);

        //                await UpdateLocalBasicUserInfo(responseObject);


        //                return responseObject;
        //            }
        //            else {
        //                return null;
        //            }
        //        }
        //        catch (Exception ex) {
        //            Debug.WriteLine($"Whoops exception: {ex.Message}");
        //            return null;
        //        }
        //    }
        //}

        //public async Task UpdateLocalBasicUserInfo(LoginResponse authenticationResponse) {

        //    var token = authenticationResponse.Token;
        //    var email = authenticationResponse.User_email;

        //    using (var client = new HttpClient()) {

        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        //        var responseOfUpdate = await client.GetAsync($"https://www.webbiter.com/index.php/wp-json/wp/v2/users?search={email}");

        //        if (responseOfUpdate.StatusCode == System.Net.HttpStatusCode.OK) {

        //            var json2 = await responseOfUpdate.Content.ReadAsStringAsync();
        //            var responseObject2 = JsonConvert.DeserializeObject<List<Root>>(json2);
        //            authenticationResponse.UserBasicInfo.Role = responseObject2[0].roles[0];
        //            authenticationResponse.UserBasicInfo.Email = responseObject2[0].email;
        //            authenticationResponse.UserBasicInfo.FirstName = responseObject2[0].first_name;
        //            authenticationResponse.UserBasicInfo.LastName = responseObject2[0].last_name;
        //            authenticationResponse.UserBasicInfo.Token = token;
        //        }

        //        if (authenticationResponse.UserBasicInfo.Role.ToLower().Contains("administrator")) {
        //            authenticationResponse.UserBasicInfo.RoleID = (int)RoleDetails.Administrator;
        //        }

        //        if (authenticationResponse.UserBasicInfo.Role.ToLower().Contains("author")) {
        //            authenticationResponse.UserBasicInfo.RoleID = (int)RoleDetails.Author;
        //        }

        //        if (authenticationResponse.UserBasicInfo.Role.ToLower().Contains("subscriber")) {
        //            authenticationResponse.UserBasicInfo.RoleID = (int)RoleDetails.Subscriber;
        //        }
        //    }
        //}
    }
}
