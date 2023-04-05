using BD_Bergeno_lituanistine_mokiklele.ViewModels.Startup;

namespace BD_Bergeno_lituanistine_mokiklele.Views.Startup;

public partial class LoadingPage : ContentPage {
    public LoadingPage(LoadingPageViewModel viewModel) {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}