using BD_Bergeno_lituanistine_mokiklele.Models;
using BD_Bergeno_lituanistine_mokiklele.Services;
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

    private readonly IRegisterService _registerService;

    public RegisterPageViewModel(IRegisterService registerService) 
    {
        _registerService = registerService;
    }


    [RelayCommand]
    async void Register() {
        if (!string.IsNullOrWhiteSpace(Email)  && !string.IsNullOrWhiteSpace(Password)
        && !string.IsNullOrWhiteSpace(ConfirmPassword) && !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName)) {
            if (!Password.Equals(ConfirmPassword))
            {
                await AppShell.Current.DisplayAlert("Slaptazodis nesutampa!",
                        "Ivesti slaptazodziai nesutampa.", "OK");
                return;
            }
            else 
            {
                var userDetails = new UserBasicInfo();
                userDetails.Email = Email;
                userDetails.FirstName = FirstName;
                userDetails.LastName = LastName;
                userDetails.Password = Password;
                userDetails.UserName = UserName;


                // calling api

                var response = await _registerService.RegisterNewUser(new RegisterRequest {
                    first_name = FirstName,
                    last_name = LastName,
                    username = UserName,
                    password = Password,
                    email = Email,
                    description = ""
                });

                if (response != null) {
                    
                    //if (response. == null) {
                    //    await AppShell.Current.DisplayAlert("No Role Assigned",
                    //        "No role assigned to this user.", "OK");
                    //    return;
                    //}
                }
                else 
                {
                    await AppShell.Current.DisplayAlert("Invalid registration!", "Sorry something vent wrong", "OK");
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
    }
