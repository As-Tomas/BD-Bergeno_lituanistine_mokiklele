namespace BD_Bergeno_lituanistine_mokiklele.ViewModels.Startup;

public class RegisterPageViewModel : ContentPage
{
	public RegisterPageViewModel()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
}