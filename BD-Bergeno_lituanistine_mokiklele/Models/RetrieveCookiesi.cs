using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BD_Bergeno_lituanistine_mokiklele.Models
{
    internal class RetriveCookies
    {
        private static readonly HttpClientHandler _clientHandler = new HttpClientHandler()
        {
            UseCookies = true,
            CookieContainer = new CookieContainer()
        };

        private static readonly HttpClient _client = new HttpClient(_clientHandler);
        public CookieContainer Cookies => _clientHandler.CookieContainer;


        public async Task AutoLogin(string token)
        {
            HttpResponseMessage responseOfUpdate = await _client.GetAsync($"https://webbiter.com/?rest_route=/simple-jwt-login/v1/autologin&JWT={token}");

            //if (responseOfUpdate.StatusCode == System.Net.HttpStatusCode.OK)
            //{

            //    var cookies = _clientHandler.CookieContainer.GetCookies(new Uri("https://webbiter.com"));

            //}
        }
    }
}

