using BD_Bergeno_lituanistine_mokiklele.Models;
using BD_Bergeno_lituanistine_mokiklele;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using static System.Net.Mime.MediaTypeNames;
using System.Net;

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
            _baseAddress = "http://www.webbiter.com";
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

        public async Task<LoginResponse> AuthenticateSimpleWay(LoginRequest loginRequest)
        {
            using (var _httpClient = new HttpClient())
            {
                try
                {
                    String simpleJWTLogin = "https://webbiter.com/?rest_route=/simple-jwt-login/v1/auth";

                    string loginRequestStr = JsonConvert.SerializeObject(loginRequest);
                    HttpResponseMessage response = await _httpClient.PostAsync(simpleJWTLogin, new StringContent(loginRequestStr,
                        Encoding.UTF8, "application/json"));

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<LoginResponse>(json);

                        await UpdateLocalBasicUserInfoSimpleWay(responseObject, loginRequest);
                        await AutoLogin(responseObject, loginRequest);

                        return responseObject;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {

                    Debug.WriteLine($"Whoops exception: {ex.Message}");
                    return null;
                }
            }
        }

        private async Task UpdateLocalBasicUserInfoSimpleWay(LoginResponse authenticationResponse, LoginRequest loginRequest)
        {

            var token = authenticationResponse.data.jwt;
            var email = loginRequest.email;
            var secrect = new Secrets(); // login secret password from WP Application Passwords converted by postman
            var password = secrect.Psw;
            var tokenSimple = secrect.bearerToken;

            using (var _httpClient = new HttpClient())
            {

                _httpClient.DefaultRequestHeaders.Add("Authorization", tokenSimple);
                HttpResponseMessage responseOfUpdate = await _httpClient.GetAsync($"{_url}/wp/v2/users?search={email}");

                if (responseOfUpdate.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    // BUG jsonStr. When login returns arr of strings, When register and try to login returns empty arr. WTF?
                    var jsonStr = await responseOfUpdate.Content.ReadAsStringAsync();
                    var responseObject2 = JsonConvert.DeserializeObject<List<Root>>(jsonStr);
                    authenticationResponse.UserBasicInfo.Role = responseObject2[0].roles[0];
                    authenticationResponse.UserBasicInfo.Email = responseObject2[0].email;
                    authenticationResponse.UserBasicInfo.FirstName = responseObject2[0].first_name;
                    authenticationResponse.UserBasicInfo.LastName = responseObject2[0].last_name;
                    authenticationResponse.UserBasicInfo.Token = token;
                }
                else
                {
                    var toast = Toast.Make("FAILED UpdateLocalBasicUserInfo", ToastDuration.Long);
                    toast.Show();
                }

                if (authenticationResponse.UserBasicInfo.Role.ToLower().Contains("administrator"))
                {
                    authenticationResponse.UserBasicInfo.RoleID = (int)RoleDetails.Administrator;
                }

                if (authenticationResponse.UserBasicInfo.Role.ToLower().Contains("author"))
                {
                    authenticationResponse.UserBasicInfo.RoleID = (int)RoleDetails.Author;
                }

                if (authenticationResponse.UserBasicInfo.Role.ToLower().Contains("subscriber"))
                {
                    authenticationResponse.UserBasicInfo.RoleID = (int)RoleDetails.Subscriber;
                }

            }

        }
        // testing auto login result true
        private async Task AutoLogin(LoginResponse authenticationResponse, LoginRequest loginRequest)
        {

            var token = authenticationResponse.data.jwt;
            var email = loginRequest.email;
            var secrect = new Secrets(); // login secret password from WP Application Passwords converted by postman
            var password = secrect.Psw;
            var tokenSimple = secrect.bearerToken;

            HttpClientHandler _clientHandler = new HttpClientHandler()
            {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };

            using (HttpClient _client = new HttpClient(_clientHandler))
            {

                //_httpClient.DefaultRequestHeaders.Add("Authorization", tokenSimple);
                HttpResponseMessage responseOfUpdate = await _client.GetAsync($"https://webbiter.com/?rest_route=/simple-jwt-login/v1/autologin&JWT={token}");

                if (responseOfUpdate.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    // BUG jsonStr. When login returns arr of strings, When register and try to login returns empty arr. WTF?
                    var jsonStr = await responseOfUpdate.Content.ReadAsStringAsync();
                    var cookies = _clientHandler.CookieContainer.GetCookies(new Uri("https://webbiter.com"));



                }

            }
        }


        private async Task UpdateLocalBasicUserInfo(LoginResponse authenticationResponse) {

            var token = authenticationResponse.Token;
            var email = authenticationResponse.User_email;
            var secrect = new Secrets(); // login secret password from WP Application Passwords converted by postman
            var password = secrect.Psw; 
            var tokenSimple = secrect.bearerToken;

            using (var _httpClient = new HttpClient()) {

                _httpClient.DefaultRequestHeaders.Add("Authorization", tokenSimple);
                HttpResponseMessage responseOfUpdate = await _httpClient.GetAsync($"{_url}/wp/v2/users?search={email}");

                if (responseOfUpdate.StatusCode == System.Net.HttpStatusCode.OK) {

                    // BUG jsonStr. When login returns arr of strings, When register and try to login returns empty arr. WTF?
                    var jsonStr = await responseOfUpdate.Content.ReadAsStringAsync();
                    var responseObject2 = JsonConvert.DeserializeObject<List<Root>>(jsonStr);
                    authenticationResponse.UserBasicInfo.Role = responseObject2[0].roles[0];
                    authenticationResponse.UserBasicInfo.Email = responseObject2[0].email;
                    authenticationResponse.UserBasicInfo.FirstName = responseObject2[0].first_name;
                    authenticationResponse.UserBasicInfo.LastName = responseObject2[0].last_name;
                    authenticationResponse.UserBasicInfo.Token = token;
                }
                else
                {
                    var toast = Toast.Make("FAILED UpdateLocalBasicUserInfo", ToastDuration.Long);
                    toast.Show();
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
