using System;
using Test.iOS.Renderer;
using Test.iOS.Views;
using UIKit;
using Xamarin.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Test.Controls.DrawView), typeof(DrawViewRenderer))]
namespace Test.iOS.Renderer
{
    public class DrawViewRenderer : ViewRenderer<Controls.DrawView, UIView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Controls.DrawView> e)
        {
            base.OnElementChanged(e);

            var drawView = new DrawViewController().View;
            
            base.SetNativeControl(drawView);
        }
    }
}