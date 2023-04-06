using BD_Bergeno_lituanistine_mokiklele.ViewModels.Startup;

namespace BD_Bergeno_lituanistine_mokiklele.Views.Startup;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}