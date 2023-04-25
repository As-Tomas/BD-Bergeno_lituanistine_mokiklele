using BD_Bergeno_lituanistine_mokiklele.Models;
using System.Net;

namespace BD_Bergeno_lituanistine_mokiklele.Views.Dashboard;

public partial class ProfileDashboardPage : ContentPage
{
	public ProfileDashboardPage()
	{
		InitializeComponent();

        //CookieContainer cookieContainer = new CookieContainer();
        //Uri uri = new Uri("https://learn.microsoft.com/dotnet/maui", UriKind.RelativeOrAbsolute);

        //Cookie cookie = new Cookie
        //{
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

        //content.Content = webView;
        //webView.Reload();
        //----------------------------------------------------------------------------------------------------------------------


        CookieContainer cookieContainer = new CookieContainer();
        Uri uri = new Uri("https://webbiter.com/index.php/2023/04/11/privatus-postas/", UriKind.RelativeOrAbsolute);
        Uri uri1 = new Uri("https://webbiter.com/index.php/2023/04/11/privatus-tik-wp/", UriKind.RelativeOrAbsolute);
        Uri uri2 = new Uri("https://webbiter.com", UriKind.RelativeOrAbsolute);
        //"PHPSESSID=f72d5d559de38e3371ce23d40f347016; " +
        //    "wc_auth_3c156e129f55ffdb2b17dbdaddc0c00e=27%7C1682498521%7C6f4aaa8fdb282998a7a3a3e9d2823a721c9b285143cc1a53180dee9d64fa5a02; " +
        //    "wordpress_logged_in_3c156e129f55ffdb2b17dbdaddc0c00e=test%7C1681463912%7CWcBBmRzRS1GGdV5qR0Y7Km32mgCnxLXwLjBTFFrlloA%7C20040ce9471076df2c4dfe0375632bdf5ec31ea4ea0a9196bfdde54faf688e3f; " +
        //    "wp-settings-time-65=1681286505"

        Cookie cookie = new Cookie
        {
            Name = "JWT",
            Secure = true,
            Value = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE2ODIxNTE3NTIsImVtYWlsIjoid3h3eHd4d3h3eEB3eHd4d3h3eC53eCJ9.cLz2npkcT7w0te7-uSm1mCISu6cP84XPt5cnMoCGCCs"
        };
        Cookie cookie1 = new Cookie
        {
            Name = "Authorization",
            Secure = true,
            Value = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE2ODIxNTE3NTIsImVtYWlsIjoid3h3eHd4d3h3eEB3eHd4d3h3eC53eCJ9.cLz2npkcT7w0te7-uSm1mCISu6cP84XPt5cnMoCGCCs"
        };

        Cookie cookie2 = new Cookie
        {
            Name = "PHPSESSID",
            Secure = true,
            Value = "f0770b5366ef713c2da03239583fd860"
        };

        Cookie cookie3 = new Cookie
        {
            Name = "wc_auth_3c156e129f55ffdb2b17dbdaddc0c00e",
            Secure = true,
            HttpOnly = true,
            Value = "6%7C1683321369%7Cef770e7bcd9378f237030cce2e4ec5c9db63c74314bf4cfdabed7630014d8790"
        };

        // sitas prijungia virsutiniu netikrinau
        Cookie cookie4 = new Cookie
        {
            Name = "wordpress_logged_in_3c156e129f55ffdb2b17dbdaddc0c00e",
            Secure = true,
            HttpOnly = true,
            Value = "tomas.bance%40gmail.com%7C1682284569%7CKu6ZrBMZlFZNIrw12CXRfHLJyOauce0rnQR1uTK3T0c%7Cd69324e1e2fd7f75cf0227eb4c2a51232ebc0c2f4d58242035f98898857169a1"
        };

        //Cookie cookie5 = new Cookie
        //{
        //    Name = "wp-settings-time-5",
        //    Secure = true,
        //    Value = "1679242613"
        //};

        //cookieContainer.Add(uri2, cookie);
       // cookieContainer.Add(uri2, cookie1);
        //cookieContainer.Add(uri2, cookie2);
        //cookieContainer.Add(uri2, cookie3);
        //cookieContainer.Add(uri2, cookie4);
        //cookieContainer.Add(uri2, cookie5);

        //----------------ading cookies retrive 
        var retriveCookies = new RetriveCookies();
        String token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE2ODI0NTY2MjcsImVtYWlsIjoidGVzdEB0dHQudHQifQ.abdYMRBAl2Itv-yfizl6mPnKpCZwY5Lv6ATvBT1LGbU";
        await retriveCookies.AutoLogin(token);

        var cookiesAutoLogin = retriveCookies.Cookies.GetCookies(new Uri("https://webbiter.com"));

        foreach( Cookie cookieO in cookiesAutoLogin)
        {
            cookieContainer.Add(uri2, cookieO);
        }

        var coo = cookieContainer.GetAllCookies();


        var webView = new WebView();
        var web = webView.Cookies;

        webView.Cookies = cookieContainer;
        webView.Source = new UrlWebViewSource { Url = uri1.ToString() };

        content.Content = webView;
        webView.Reload();


    }

}