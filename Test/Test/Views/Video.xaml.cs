using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Test.Controls;
using Test.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Video : ContentPage
	{
	    public string VideoPath;
		public Video ()
		{
		    Title = "Video";
            InitializeComponent();

		    //LoadVideoHtml();
		}

	    private void LoadVideoHtml()
	    {
            //var assetManager = Forms.Context.Assets;
            //var htmlString = string.Empty;
            //using (var streamReader = new StreamReader(assetManager.Open("index.html")))
            //{
            //    htmlString = streamReader.ReadToEnd();
            //}


            //var html = new HtmlWebViewSource
            //{
            //    Html = htmlString
            //};

            //var xWebview = new WebView
            //{
            //    Source = html,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    VerticalOptions = LayoutOptions.FillAndExpand
            //};

            ////VideoContent.Children.Add(xWebview);

            //var htmlFile = @"file:///android_asset/index.html";
            //var webView = new Android.Webkit.WebView(Forms.Context);
            //var webSettings = webView.Settings;
            //webSettings.JavaScriptEnabled = true;
            //webSettings.AllowFileAccessFromFileURLs = true;
            //webSettings.AllowUniversalAccessFromFileURLs = true;

            //webView.AddJavascriptInterface(null, "Android");

            //webView.SetWebViewClient(new Android.Webkit.WebViewClient());

            //webView.SetWebChromeClient(new ChromeClient());
            //webView.LoadUrl("www.google.com.au");

            //var contentView = new ContentView();
            //contentView.Content = webView.ToView();
            //VideoContent.Children.Add(contentView);
        }
    }
}