using Android.Webkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RateListing.App
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Xamarin.Forms.WebView browser = new Xamarin.Forms.WebView();

        public MainPage()
        {
            InitializeComponent();

            browser = new Xamarin.Forms.WebView
            {
                Source = "https://www.ratelisting.com?platform=mobileApp",
            };

            Content = browser;
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            browser.GoBack();
            return true;
        }
    }
}
