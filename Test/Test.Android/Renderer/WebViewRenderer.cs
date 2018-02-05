using System.IO;
using Xamarin.Forms.Platform.Android;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.IO;
using Test.Droid.Renderer;
using Test.Droid.Repos;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Test.Controls.WebView), typeof(Test.Droid.Renderer.WebViewRenderer))]
namespace Test.Droid.Renderer
{
    public class WebViewRenderer : ViewRenderer<Controls.WebView, Android.Webkit.WebView>
    {
        Android.Webkit.WebView _webview;
        public WebViewRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Test.Controls.WebView> e)
        {
            base.OnElementChanged(e);

            _webview = new Android.Webkit.WebView(Context);

            var urlOrFile = e.NewElement.HtmlSource;

            var webSettings = _webview.Settings;
            webSettings.JavaScriptEnabled = true;
            webSettings.AllowFileAccessFromFileURLs = true;
            webSettings.AllowUniversalAccessFromFileURLs = true;

            //_webview.AddJavascriptInterface(null, "Android");

            _webview.SetWebViewClient(new WebViewClient());
            _webview.SetWebChromeClient(new ChromeClient());
            _webview.LoadUrl(urlOrFile);

            base.SetNativeControl(_webview);
        }
    }
}