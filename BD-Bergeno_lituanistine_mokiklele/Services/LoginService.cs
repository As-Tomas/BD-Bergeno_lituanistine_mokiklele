using BD_Bergeno_lituanistine_mokiklele.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BD_Bergeno_lituanistine_mokiklele.Services {
    public class LoginService : ILoginService {


        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly string _apiKey;

        public LoginService() {
            //_httpClient = new HttpClient(); kai sita cia tai lieka hederis kuri funkcijoje suformuoju ir issiloginus loginantis neleidzia naujo hederio formuoti
            //_baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2.:5209" : "http://localhost:7209 "
            _baseAddress = "https://www.webbiter.com";
            _url = $"{_baseAddress}/index.php/wp-json";

            _jsonSerializerOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

        }

        public async Task<LoginResponse> Authenticate(LoginRequest loginRequest) {
            using (var _httpClient = new HttpClient()) {


                try {
                    string loginRequestStr = JsonConvert.SerializeObject(loginRequest);
                    HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/jwt-auth/v1/token", new StringContent(loginRequestStr,
                        Encoding.UTF8, "application/json"));

                    if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                        
                        var json = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<LoginResponse>(json);

                         await UpdateLocalBasicUserInfo(responseObject);


                        return responseObject;
                    }
                    else {
                        return null;
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine($"Whoops exception: {ex.Message}");
                    return null;
                }
            }
        }
        
        public async Task UpdateLocalBasicUserInfo(LoginResponse authenticationResponse) {

            var token = authenticationResponse.Token;
            var email = authenticationResponse.User_email;

            using (var Client = new HttpClient()) {

                Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage responseOfUpdate = await Client.GetAsync($"{_url}/wp/v2/users?search={email}");

                if (responseOfUpdate.StatusCode == System.Net.HttpStatusCode.OK) {

                    var json2 = await responseOfUpdate.Content.ReadAsStringAsync();
                    var responseObject2 = JsonConvert.DeserializeObject<List<Root>>(json2);
                    authenticationResponse.UserBasicInfo.Role = responseObject2[0].roles[0];
                    authenticationResponse.UserBasicInfo.Email = responseObject2[0].email;
                    authenticationResponse.UserBasicInfo.FirstName = responseObject2[0].first_name;
                    authenticationResponse.UserBasicInfo.LastName = responseObject2[0].last_name;
                    authenticationResponse.UserBasicInfo.Token = token;
                }

                if (authenticationResponse.UserBasicInfo.Role.ToLower().Contains("administrator")) {
                    authenticationResponse.UserBasicInfo.RoleID = (int)RoleDetails.Administrator;
                }

                if (authenticationResponse.UserBasicInfo.Role.ToLower().Contains("author")) {
                    authenticationResponse.UserBasicInfo.RoleID = (int)RoleDetails.Author;
                }

                if (authenticationResponse.UserBasicInfo.Role.ToLower().Contains("subscriber")) {
                    authenticationResponse.UserBasicInfo.RoleID = (int)RoleDetails.Subscriber;
                }
                
            }
            
        }


        //public async Task<LoginResponse> Authenticate(LoginRequest loginRequest) {
        //    using (var client = new HttpClient()) {
        //        string loginRequestStr = JsonConvert.SerializeObject(loginRequest);

        //        var response = await client.PostAsync("https://www.webbiter.com/wp-json/jwt-auth/v1/token",
        //              new StringContent(loginRequestStr, Encoding.UTF8));

        //        if (response.StatusCode == System.Net.HttpStatusCode.OK) {
        //            var json = await response.Content.ReadAsStringAsync();
        //            return JsonConvert.DeserializeObject<LoginResponse>(json);
        //        }
        //        else {
        //            return null;
        //        }
        //    }
        //}

        //public async Task<List<UserListResponse>> GetAllUsers() {
        //    using (var client = new HttpClient()) {
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.Token);
        //        var response = await client.GetAsync("http://192.168.0.185/User/GetAllUsers");

        //        if (response.StatusCode == System.Net.HttpStatusCode.OK) {
        //            var json = await response.Content.ReadAsStringAsync();
        //            return JsonConvert.DeserializeObject<List<UserListResponse>>(json);
        //        }
        //        else {
        //            return null;
        //        }
        //    }
        //}
    }

    
    
}
