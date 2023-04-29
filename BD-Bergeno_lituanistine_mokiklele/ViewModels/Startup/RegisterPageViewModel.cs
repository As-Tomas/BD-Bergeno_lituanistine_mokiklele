using BD_Bergeno_lituanistine_mokiklele.Models;
using BD_Bergeno_lituanistine_mokiklele.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

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

    [ObservableProperty]
    private string _description;

    private readonly IRegisterService _registerService;
    private readonly ILoginService _loginService;

    public RegisterPageViewModel(IRegisterService registerService, ILoginService loginService) 
    {
        _registerService = registerService;
        _loginService = loginService;
    }


    [RelayCommand]
    async void Register() {

        if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password)
            && !string.IsNullOrWhiteSpace(ConfirmPassword) && !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName)) {
            if (!Password.Equals(ConfirmPassword)) {

                await AppShell.Current.DisplayAlert("Slaptazodis nesutampa!",
                        "Ivesti slaptazodziai nesutampa.", "OK");
                return;
            }
            else {
                // calling api

                var response = await _registerService.RegisterNewUser(new RegisterRequest {
                    first_name = FirstName,
                    last_name = LastName,
                    username = UserName,
                    password = Password,
                    email = Email,
                    description = Description
                });

                if (response != null) {
                    Int32 responseCode = (Int32)response.code;

                    if (responseCode.Equals(200)) {
                        var registrationSuccess = Toast.Make("Registracija sekminga", ToastDuration.Long);
                        await registrationSuccess.Show();

                        // login with new user account
                        var loginResponse = await _loginService.Authenticate(new LoginRequest {
                            email = Email,
                            password = Password
                        });
                        if (loginResponse != null) {

                            if (Preferences.ContainsKey(nameof(App.UserDetails))) {
                                Preferences.Remove(nameof(App.UserDetails));
                            }

                            string userDetailStr = JsonConvert.SerializeObject(loginResponse.UserBasicInfo);
                            Preferences.Set(nameof(App.UserDetails), userDetailStr);
                            App.UserDetails = loginResponse.UserBasicInfo;
                            App.UserDetails.Token = loginResponse.Token;
                            await AppConstant.AddFlyoutMenusDetails();
                        }
                    }
                    else 
                    {
                        var registrationFailure = Toast.Make($"{response.message}", ToastDuration.Long);
                        await registrationFailure.Show();
                    }

                }
                else {
                    await AppShell.Current.DisplayAlert("Invalid registration!", "Sorry something vent wrong", "OK");
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
        else {
            var toast = Toast.Make("Užpildykite visus privalomus laukus", ToastDuration.Long);
            await toast.Show();
        }
        
        }
    }
