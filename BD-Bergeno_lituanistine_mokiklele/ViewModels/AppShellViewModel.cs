
using CommunityToolkit.Mvvm.ComponentModel;
using BD_Bergeno_lituanistine_mokiklele.Views.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace BD_Bergeno_lituanistine_mokiklele.ViewModels {
    public partial class AppShellViewModel : BaseViewModel {

        [RelayCommand]
        async void SignOut() {
            if (Preferences.ContainsKey(nameof(App.UserDetails))) {
                Preferences.Remove(nameof(App.UserDetails));
            }
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}