using BD_Bergeno_lituanistine_mokiklele.Platforms.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BD_Bergeno_lituanistine_mokiklele.Services
{
    public class RestDataService : IRestDataService {

       private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly string _apiKey;
        public RestDataService()
        {
            _httpClient = new HttpClient();
            //_baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2.:5209" : "http://localhost:7209 "
            _baseAddress = "https://www.webbiter.com/";
            _url = $"{_baseAddress}/index.php/wp-json/wp/v2/";

            _jsonSerializerOptions = new JsonSerializerOptions 
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

    }
    public async Task<List<Post>> GetAllPostsAsync() 
        {
            List<Post> posts = new List<Post>();

            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet) 
            {
                Debug.WriteLine("----> No internet access...");
                return posts;
            }

            try 
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/posts");

                if (response.IsSuccessStatusCode)
                {
                    //string content = await response.Content.ReadAsStringAsync();

                    //posts = JsonSerializer.Deserialize<List<Post>>(content, _jsonSerializerOptions);


                    posts = await response.Content.ReadFromJsonAsync<List<Post>>();
                }
                else {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }
            return posts;
        }
    }
}
