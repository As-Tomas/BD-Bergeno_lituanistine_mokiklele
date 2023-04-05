
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm;
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
    public partial class LoginPageViewModel : BaseViewModel 
        {
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        #region Commands
        [RelayCommand]
        async void Login() {
            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password)) {
                var userDetails = new UserBasicInfo();
                userDetails.Email = Email;
                userDetails.FullName = "Test User Name";

                // Narys Role, Teacher Role, Admin Role,
                if (Email.ToLower().Contains("narys")) {
                    userDetails.RoleID = (int)RoleDetails.Narys;
                    userDetails.RoleText = "Narys";
                }
                else if (Email.ToLower().Contains("redaktorius")) {
                    userDetails.RoleID = (int)RoleDetails.Redaktorius;
                    userDetails.RoleText = "Redaktorius";
                }
                else {
                    userDetails.RoleID = (int)RoleDetails.Administratorius;
                    userDetails.RoleText = "Administratorisu";
                }


                // calling api 


                if (Preferences.ContainsKey(nameof(App.UserDetails))) {
                    Preferences.Remove(nameof(App.UserDetails));
                }

                string userDetailStr = JsonConvert.SerializeObject(userDetails);
                Preferences.Set(nameof(App.UserDetails), userDetailStr);
                App.UserDetails = userDetails;
                await AppConstant.AddFlyoutMenusDetails();
            }


        }
        #endregion
    }
}