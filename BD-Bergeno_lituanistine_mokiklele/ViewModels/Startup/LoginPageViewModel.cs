
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
using BD_Bergeno_lituanistine_mokiklele.Services;

namespace BD_Bergeno_lituanistine_mokiklele.ViewModels.Startup {
    public partial class LoginPageViewModel : BaseViewModel 
        {
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        private readonly ILoginService _loginService;
        public LoginPageViewModel(ILoginService loginService) {
            _loginService = loginService;
        }

        #region Commands
        [RelayCommand]
        async void Login() {
            if (true)
            {
                //if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password)) {


                //var userDetails = new UserBasicInfo();
                //userDetails.Email = Email;
                //userDetails.FirstName = "First Name";

                //// Narys Role, Teacher Role, Admin Role,
                //if (Email.ToLower().Contains("narys")) {
                //    userDetails.RoleID = (int)RoleDetails.Narys;
                //    userDetails.Role = "Narys";
                //}
                //else if (Email.ToLower().Contains("redaktorius")) {
                //    userDetails.RoleID = (int)RoleDetails.Redaktorius;
                //    userDetails.Role = "Redaktorius";
                //}
                //else {
                //    userDetails.RoleID = (int)RoleDetails.Administratorius;
                //    userDetails.Role = "Administratorisu";
                //}


                // calling api 
                //var response = await _loginService.Authenticate(new LoginRequest {
                //    username = Email,
                //    password = Password

                //});

                var secrets = new Secrets();
                var response = await _loginService.AuthenticateSimpleWay(new LoginRequest
                {
                    //username = secrets.usrEmail,
                    //password = secrets.usrPasword
                    email = secrets.usrEmail,
                    password = secrets.usrPasword

                });

                if (response != null) {

                    if (response.UserBasicInfo.Role == null) {
                        await AppShell.Current.DisplayAlert("No Role Assigned",
                            "No role assigned to this user.", "OK");

                        // update user info

                        return;
                    }
                    

                    if (Preferences.ContainsKey(nameof(App.UserDetails))) {
                        Preferences.Remove(nameof(App.UserDetails));
                    }

                    //await SecureStorage.SetAsync(nameof(App.UserDetails.Token), response.Token);

                    string userDetailStr = JsonConvert.SerializeObject(response.UserBasicInfo);
                    Preferences.Set(nameof(App.UserDetails), userDetailStr);
                    App.UserDetails = response.UserBasicInfo;
                    App.UserDetails.Token = response.Token;
                    await AppConstant.AddFlyoutMenusDetails();

                }
                else {
                    await AppShell.Current.DisplayAlert("Invalid User Name Or Password", "Invalid UserName or Password", "OK");
                }



                //if (Preferences.ContainsKey(nameof(App.UserDetails))) {
                //    Preferences.Remove(nameof(App.UserDetails));
                //}

                //string userDetailStr = JsonConvert.SerializeObject(userDetails);
                //Preferences.Set(nameof(App.UserDetails), userDetailStr);
                //App.UserDetails = userDetails;
                //await AppConstant.AddFlyoutMenusDetails();
            }
        }

        [RelayCommand]
         async void GoToRegister() {

            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");

            ////await DisplayAlert("Tapped", "This is a tapped Span.", "OK")
            //AppShell.Current.Dispatcher.Dispatch( () => {
            //     Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
            
            //});
        }
        #endregion

        //[RelayCommand]
        //async void OnTapGestureRecognizerTapped(ItemTappedEventArgs e) {
        //    // Handle the tap
        //    await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        //}
        


    }
}