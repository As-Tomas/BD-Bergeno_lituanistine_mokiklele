using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD_Bergeno_lituanistine_mokiklele.Models;
using BD_Bergeno_lituanistine_mokiklele.Views.Dashboard;
using BD_Bergeno_lituanistine_mokiklele.Views.Startup;
using System.Reflection;
using CommunityToolkit.Mvvm.Input;

namespace BD_Bergeno_lituanistine_mokiklele.ViewModels.Dashboard
{
    public partial class ProfileDashboardPageViewModel
    {
        public ProfileDashboardPageViewModel()
        {
            LoadCookies();
        }

        [RelayCommand]
        public async void LoadCookies()
        {
            string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");

            //if (Preferences.ContainsKey(nameof(App.UserDetails))) {
            //    Preferences.Remove(nameof(App.UserDetails));
            //}


            if (string.IsNullOrWhiteSpace(userDetailsStr))
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    //AppShell.Current.Dispatcher.Dispatch(async () => {
                    //    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    //});
                }
                else
                {
                    //await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
            }
            else
            {
                var cookiesString = App.UserDetails.Cookies;
                var userCookies = JsonConvert.DeserializeObject<List<CookiesResponse>>(cookiesString);

                string json = JsonConvert.SerializeObject(userCookies, Formatting.Indented);
                Console.WriteLine(json);               
                
            }
        }
    }
}
