using BD_Bergeno_lituanistine_mokiklele.Models;
using BD_Bergeno_lituanistine_mokiklele.ViewModels.Dashboard;
using Newtonsoft.Json;
using System.Net;

namespace BD_Bergeno_lituanistine_mokiklele.Views.Dashboard;

public partial class ProfileDashboardPage : ContentPage
{
    private WebView MyWebView;
    public ProfileDashboardPage()
    {
        InitializeComponent();
        this.BindingContext = new ProfileDashboardPageViewModel();
    }

    private WebView CreateNewWebView()
    {
        return new WebView();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Create a new instance of the WebView control and assign it to the existing reference.
        MyWebView = CreateNewWebView();
    
        Uri uri1 = new Uri("https://webbiter.com/index.php/2023/04/11/privatus-postas/", UriKind.RelativeOrAbsolute);
        Uri uri2 = new Uri("https://webbiter.com/index.php/2023/04/11/privatus-tik-wp/", UriKind.RelativeOrAbsolute);
        Uri uri3 = new Uri("https://webbiter.com", UriKind.RelativeOrAbsolute);
        
        Uri uri = new Uri("https://webbiter.com/index.php/my-profile/", UriKind.RelativeOrAbsolute);

        var cookiesString = App.UserDetails.Cookies;
        var userCookies = JsonConvert.DeserializeObject<List<CookiesResponse>>(cookiesString);
        
        CookieContainer cookieContainer = new CookieContainer();

        for (int i = 0; i < userCookies.Count; i++)
        {
            var coockie = new Cookie
            {
                Name = userCookies[i].Name,
                Secure = userCookies[i].Secure,
                Value = userCookies[i].Value,
                Expires = userCookies[i].Expires,
                HttpOnly = userCookies[i].HttpOnly,
                Domain = userCookies[i].Domain,
            };
            cookieContainer.Add(coockie);
        }

        MyWebView.Cookies = cookieContainer;
        MyWebView.Source = new UrlWebViewSource { Url = uri.ToString() };

        content.Content = MyWebView;
        MyWebView.Reload();
    }
}

