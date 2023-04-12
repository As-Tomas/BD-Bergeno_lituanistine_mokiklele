using System.Net;

namespace BD_Bergeno_lituanistine_mokiklele.Views.Dashboard;

public partial class ProfileDashboardPage : ContentPage
{
	public ProfileDashboardPage()
	{
		InitializeComponent();

        //CookieContainer cookieContainer = new CookieContainer();
        //Uri uri = new Uri("https://learn.microsoft.com/dotnet/maui", UriKind.RelativeOrAbsolute);

        //Cookie cookie = new Cookie {
        //    Name = "DotNetMAUICookie",
        //    Expires = DateTime.Now.AddDays(2),
        //    Value = "My cookie",
        //    Domain = uri.Host,
        //    Path = "/"
        //};
        //cookieContainer.Add(uri, cookie);


        //var webView = new WebView();

        //webView.Cookies = cookieContainer;
        //webView.Source = new UrlWebViewSource { Url = uri.ToString() };

        
        CookieContainer cookieContainer = new CookieContainer();
        //Uri uri = new Uri("https://webbiter.com/index.php/2023/04/11/privatus-postas/", UriKind.RelativeOrAbsolute);
        Uri uri = new Uri("https://webbiter.com/index.php/2023/04/11/privatus-tik-wp/", UriKind.RelativeOrAbsolute);
        Uri uri2 = new Uri("https://webbiter.com", UriKind.RelativeOrAbsolute);
        //"PHPSESSID=f72d5d559de38e3371ce23d40f347016; " +
        //    "wc_auth_3c156e129f55ffdb2b17dbdaddc0c00e=27%7C1682498521%7C6f4aaa8fdb282998a7a3a3e9d2823a721c9b285143cc1a53180dee9d64fa5a02; " +
        //    "wordpress_logged_in_3c156e129f55ffdb2b17dbdaddc0c00e=test%7C1681463912%7CWcBBmRzRS1GGdV5qR0Y7Km32mgCnxLXwLjBTFFrlloA%7C20040ce9471076df2c4dfe0375632bdf5ec31ea4ea0a9196bfdde54faf688e3f; " +
        //    "wp-settings-time-65=1681286505"

        Cookie cookie = new Cookie {
            Name = "JWT",
            Secure = true,
            Value = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE2ODEyOTA1NTMsImVtYWlsIjoidGVzdEB0dHQudHQifQ.AzRGu28iZa6-nQKMSTTlLRHGA_7hRNZ70RnXQGQy0Xo"
        };
        Cookie cookie1 = new Cookie {
            Name = "jwt",
            Secure = true,
            Value = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE2ODEyOTA1NTMsImVtYWlsIjoidGVzdEB0dHQudHQifQ.AzRGu28iZa6-nQKMSTTlLRHGA_7hRNZ70RnXQGQy0Xo"
        };

        Cookie cookie2 = new Cookie {
            Name = "PHPSESSID",
            Secure = true,
            Value = "f72d5d559de38e3371ce23d40f347016"
        };

        Cookie cookie3 = new Cookie {
            Name = "wc_auth_3c156e129f55ffdb2b17dbdaddc0c00e",
            Secure = true,
            Value = "27%7C1682498521%7C6f4aaa8fdb282998a7a3a3e9d2823a721c9b285143cc1a53180dee9d64fa5a02"
        };

        Cookie cookie4 = new Cookie {
            Name = "wordpress_logged_in_3c156e129f55ffdb2b17dbdaddc0c00e",
            Secure = true,
            Value = "test%7C1681463912%7CWcBBmRzRS1GGdV5qR0Y7Km32mgCnxLXwLjBTFFrlloA%7C20040ce9471076df2c4dfe0375632bdf5ec31ea4ea0a9196bfdde54faf688e3f"
        };

        Cookie cookie5 = new Cookie {
            Name = "wp-settings-time-65",
            Secure = true,
            Value = "1681286505"
        };
        //cookieContainer.Add(uri2, cookie);
        //cookieContainer.Add(uri2, cookie1);
        //cookieContainer.Add(uri2,cookie2);
        //cookieContainer.Add(uri2,cookie3);
        //cookieContainer.Add(uri2, cookie4);
        //cookieContainer.Add(uri2,cookie5);

        cookieContainer.Add(uri2, cookie);
        cookieContainer.Add(uri2, cookie1);
        cookieContainer.Add(uri2, cookie2);
        cookieContainer.Add(uri2, cookie3);
        cookieContainer.Add(uri2, cookie4);
        cookieContainer.Add(uri2, cookie5);

        var coo = cookieContainer.GetAllCookies();
        

        var webView = new WebView();

        //webView.Cookies = cookieContainer;
        webView.Cookies.Add(cookie1);
        webView.Cookies.Add(cookie);
        webView.Source = new UrlWebViewSource { Url = uri.ToString() };

        content.Content = webView;
        webView.Reload();

        
    }

}