using BD_Bergeno_lituanistine_mokiklele.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace BD_Bergeno_lituanistine_mokiklele.ViewModels.Startup;

public partial class RegisterPageViewModel : BaseViewModel {
    
        [ObservableProperty]
        private string _firstName;

    [ObservableProperty]
    private string _lastName;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _userName;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private string _confirmPassword;


    [RelayCommand]
    async void Register() {
            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password)
            && !string.IsNullOrWhiteSpace(ConfirmPassword) && !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName)) {
                var userDetails = new UserBasicInfo();
                userDetails.Email = Email;
                userDetails.FirstName = FirstName;
                userDetails.LastName = LastName;
                userDetails.Password = Password;
                userDetails.UserName = UserName;


            // calling api

            // Subscriber Role, Teacher Role, Admin Role,
            if (Email.ToLower().Contains("narys")) {
                    userDetails.RoleID = (int)RoleDetails.Subscriber;
                    userDetails.Role = "Subscriber";
                }
                else if (Email.ToLower().Contains("redaktorius")) {
                    userDetails.RoleID = (int)RoleDetails.Author;
                    userDetails.Role = "Author";
                }
                else {
                    userDetails.RoleID = (int)RoleDetails.Administrator;
                    userDetails.Role = "Administrator";
                }

 


            if (Preferences.ContainsKey(nameof(App.UserDetails))) {
                    Preferences.Remove(nameof(App.UserDetails));
                }

                string userDetailStr = JsonConvert.SerializeObject(userDetails);
                Preferences.Set(nameof(App.UserDetails), userDetailStr);
                App.UserDetails = userDetails;
                await AppConstant.AddFlyoutMenusDetails();
            }
        }
    }
