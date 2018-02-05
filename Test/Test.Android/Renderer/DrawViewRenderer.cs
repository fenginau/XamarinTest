using System.IO;
using System;
using Xamarin.Forms.Platform.Android;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Test.Droid.Renderer;
using Xamarin.Controls;
using Xamarin.Forms;
using Color = Android.Graphics.Color;
using RelativeLayout = Android.Widget.RelativeLayout;
using Button = Android.Widget.Button;
using Orientation = Android.Widget.Orientation;
using Console = System.Console;

[assembly: ExportRenderer(typeof(Test.Controls.DrawView), typeof(DrawViewRenderer))]
namespace Test.Droid.Renderer
{
    public class DrawViewRenderer : ViewRenderer<Controls.DrawView, LinearLayout>
    {
        private SignaturePadView _signature;
        public DrawViewRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.DrawView> e)
        {
            base.OnElementChanged(e);

            _signature = CreateSignaturePad();

            var okButton = CreateSaveButton();

            var layout = new LinearLayout(Context)
            {
                LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent),
                Orientation = Orientation.Vertical
            };

            layout.AddView(okButton);
            layout.AddView(_signature);

            base.SetNativeControl(layout);
        }

        public SignaturePadView CreateSignaturePad()
        {
            var signature = new SignaturePadView(Context)
            {
                StrokeWidth = 3f,
                StrokeColor = Color.Black,
                BackgroundColor = Color.White,
            };

            signature.Caption.Text = "Authorization Signature";
            signature.Caption.SetTypeface(Typeface.Serif, TypefaceStyle.BoldItalic);
            signature.Caption.SetTextSize(global::Android.Util.ComplexUnitType.Sp, 16f);
            signature.SignaturePrompt.Text = ">";
            signature.SignaturePrompt.SetTypeface(Typeface.SansSerif, TypefaceStyle.Normal);
            signature.SignaturePrompt.SetTextSize(global::Android.Util.ComplexUnitType.Sp, 32f);

            signature.BackgroundImageView.SetAdjustViewBounds(true);

            var layout = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            layout.AddRule(LayoutRules.CenterInParent);
            layout.SetMargins(20, 20, 20, 20);
            signature.BackgroundImageView.LayoutParameters = layout;

            var caption = signature.Caption;
            caption.SetPadding(caption.PaddingLeft, 1, caption.PaddingRight, 25);

            return signature;
        }

        public Button CreateSaveButton()
        {
            var button = new Button(Context)
            {
                Text = "Save"
            };

            button.Click += delegate {
                if (_signature.IsBlank)
                {
                    // display the base line for the user to sign on.
                    AlertDialog.Builder alert = new AlertDialog.Builder(Context);
                    alert.SetMessage("No signature to save.");
                    alert.SetNeutralButton("Okay", delegate { });
                    alert.Create().Show();
                }
                else
                {
                    try
                    {
                        var points = _signature.Points;
                        Bitmap image = _signature.GetImage();

                        var path1 = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);

                        var fileName = $"Signature{DateTime.Now.Ticks}.png";
                        var filePath1 = System.IO.Path.Combine(path1.AbsolutePath, fileName);

                        using (var stream = new FileStream(filePath1, FileMode.Create))
                        {
                            image.Compress(Bitmap.CompressFormat.Png, 95, stream);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            };
            return button;
        }
    }
}