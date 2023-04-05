using BD_Bergeno_lituanistine_mokiklele.ViewModels;
using BD_Bergeno_lituanistine_mokiklele.Models;
using BD_Bergeno_lituanistine_mokiklele.Views.Dashboard;

namespace BD_Bergeno_lituanistine_mokiklele;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        this.BindingContext = new AppShellViewModel();
    }
}
