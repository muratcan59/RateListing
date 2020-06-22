using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Android.Webkit;
using RateListing.App;
using Xamarin.Forms;
using RateListing.App.Droid;
using Android.Content;
using System.Deployment.Internal;

[assembly: ExportRenderer(typeof(Xamarin.Forms.WebView), typeof(GeoWebViewRenderer))]
namespace RateListing.App.Droid
{
    [Activity(Label = "RateListing", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


    }

    [Obsolete]
    public class GeoWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            Control.SetWebViewClient(new GeoWebViewClient());
            Control.SetWebChromeClient(new GeoWebChromeClient());

            Control.Settings.JavaScriptCanOpenWindowsAutomatically = true;
            Control.Settings.DomStorageEnabled = true;
            Control.Settings.JavaScriptEnabled = true;
            Control.Settings.SetGeolocationEnabled(true);

        }


    }


    public class GeoWebChromeClient : WebChromeClient
    {
        public override void OnGeolocationPermissionsShowPrompt(string origin, GeolocationPermissions.ICallback callback)
        {
            callback.Invoke(origin, true, false);
        }

    }

    public class GeoWebViewClient : WebViewClient
    {
        public override void OnReceivedSslError(Android.Webkit.WebView view, SslErrorHandler handler, Android.Net.Http.SslError er)
        {
            // Ignore SSL certificate errors
            handler.Proceed();
        }
        [Obsolete]
        public override bool ShouldOverrideUrlLoading(Android.Webkit.WebView webView, string url)
        {
            if (url.StartsWith("mailto:"))
            {
                url = url.Replace("mailto:", "");

                Intent mail = new Intent(Intent.ActionSend);
                mail.SetType("application/octet-stream");
                mail.PutExtra(Intent.ExtraEmail, new String[] { url.Split('?')[0] });
                webView.Context.StartActivity(mail);
                return true;
            }
            if (url.StartsWith("tel:"))
            {
                var uri = Android.Net.Uri.Parse(url);
                Intent phone = new Intent(Intent.ActionDial, uri);
                webView.Context.StartActivity(phone);
                return true;
            }
            return false;
        }
    }
}