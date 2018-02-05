using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Test.Controls
{
    public class WebView : View
    {
        public WebView()
        {

        }

        public static readonly BindableProperty HtmlSourceProperty =
            BindableProperty.Create(nameof(HtmlSource), typeof(string), typeof(WebView), null);

        public string HtmlSource
        {
            get => (string)GetValue(HtmlSourceProperty);
            set => SetValue(HtmlSourceProperty, value);
        }
    }
}
