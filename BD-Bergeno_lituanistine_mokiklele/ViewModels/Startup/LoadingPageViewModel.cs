using Newtonsoft.Json;
using BD_Bergeno_lituanistine_mokiklele.Controls;
using BD_Bergeno_lituanistine_mokiklele.Models;
using BD_Bergeno_lituanistine_mokiklele.Views.Dashboard;
using BD_Bergeno_lituanistine_mokiklele.Views.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Bergeno_lituanistine_mokiklele.ViewModels.Startup {
    public class LoadingPageViewModel {
        public LoadingPageViewModel() {
            CheckUserLoginDetails();
        }
        private async void CheckUserLoginDetails() {
            string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");

            //if (Preferences.ContainsKey(nameof(App.UserDetails))) {
            //    Preferences.Remove(nameof(App.UserDetails));
            //}

            if (string.IsNullOrWhiteSpace(userDetailsStr)) {
                if (DeviceInfo.Platform == DevicePlatform.WinUI) {
                    AppShell.Current.Dispatcher.Dispatch(async () => {
                        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    });
                }
                else {
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
                // navigate to Login Page
            }
            else {
                var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetailsStr);
                App.UserDetails = userInfo;
                await AppConstant.AddFlyoutMenusDetails();
            }
        }

    }
}