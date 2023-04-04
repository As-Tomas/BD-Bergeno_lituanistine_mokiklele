
using BD_Bergeno_lituanistine_mokiklele.Services;
using Microsoft.Maui.Controls.Internals;

namespace BD_Bergeno_lituanistine_mokiklele;

public partial class MainPage : ContentPage
{
    private readonly IRestDataService _dataService;

    // int count = 0;

    /*public MainPage()
	{
		InitializeComponent();
		string source = "https://webbiter.com/";
		var mv = new WebView();
		content.Content = mv;
		mv.Source = source;
		mv.Reload();

    }*/

    public MainPage(IRestDataService dataService) {
        InitializeComponent();

        _dataService = dataService;
    }

    protected async override void OnAppearing() {
        base.OnAppearing();

        collectionView.ItemsSource = await _dataService.GetAllPostsAsync();
    }
}


//<CollectionView>
//        <CollectionView.ItemsSource>
//            <x:Array Type = "{x:Type model:Post}" >
//                < model:Post Title = "Pavadinimas" />
//                < model:Post Title = "Pavadinimas2" />
//            </ x:Array>
//        </CollectionView.ItemsSource>
//    </CollectionView>
