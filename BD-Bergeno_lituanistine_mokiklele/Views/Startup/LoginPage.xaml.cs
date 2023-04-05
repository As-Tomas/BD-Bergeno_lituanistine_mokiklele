using BD_Bergeno_lituanistine_mokiklele.ViewModels.Startup;

namespace BD_Bergeno_lituanistine_mokiklele.Views.Startup;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}