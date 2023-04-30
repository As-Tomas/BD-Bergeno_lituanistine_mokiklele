using BD_Bergeno_lituanistine_mokiklele.Models;
using Newtonsoft.Json;
using System.Net;

namespace BD_Bergeno_lituanistine_mokiklele.Views.WebViewPages;

public partial class MessagesPage : ContentPage
{
	private WebView MyWebView;
	public MessagesPage()
	{
		InitializeComponent();
	}
    private WebView CreateNewWebView()
    {
        return new WebView();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        MyWebView = CreateNewWebView();       

        Uri uri = new Uri("https://webbiter.com/index.php/zinutes/", UriKind.RelativeOrAbsolute);

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