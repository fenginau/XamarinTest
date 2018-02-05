using System;
using CoreGraphics;
using UIKit;
using Xamarin.Controls;

namespace Test.iOS.Views
{
    class DrawViewController : UIViewController
    {

        private SignaturePadView _signature;
        public DrawViewController()
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _signature = CreateSignaturePad();

            View.BackgroundColor = UIColor.White;
            Title = "My Custom View Controller";

            var btn = CreateSaveButton();

            View.AddSubview(btn);
            View.AddSubview(_signature);

        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            _signature.Frame = new CGRect(0, 60, View.Bounds.Width, View.Bounds.Height * 0.6);

        }

        public SignaturePadView CreateSignaturePad()
        {
            var signaturepad = new SignaturePadView();
            signaturepad.Caption.Font = UIFont.FromName("Marker Felt", 16f);
            signaturepad.CaptionText = "Authorization Signature";
            signaturepad.SignaturePromptText = "☛";
            signaturepad.SignaturePrompt.Font = UIFont.FromName("Helvetica", 32f);
            signaturepad.BackgroundColor = UIColor.White;
            //signaturepad.BackgroundImageView.Image = UIImage.FromBundle("logo-galaxy-black-64.png");
            //signaturepad.BackgroundImageView.Alpha = 0.0625f;
            //signaturepad.BackgroundImageView.ContentMode = UIViewContentMode.ScaleToFill;
            //signaturepad.BackgroundImageView.Frame = new System.Drawing.RectangleF(20, 20, 256, 256);
            signaturepad.Layer.ShadowOffset = new System.Drawing.SizeF(0, 0);
            signaturepad.Layer.ShadowOpacity = 1f;
            signaturepad.Frame = new CGRect(0, 60, View.Bounds.Width, View.Bounds.Height * 0.6);
            return signaturepad;
        }

        public UIButton CreateSaveButton()
        {
            var btn = UIButton.FromType(UIButtonType.System);
            btn.Frame = new CGRect(0, 0, View.Bounds.Width, 44);
            btn.SetTitle("Save", UIControlState.Normal);

            btn.TouchUpInside += (sender, e) => {
                if (_signature.IsBlank)
                {
                    var okAlertController = UIAlertController.Create("OK Alert", "No signature to save.", UIAlertControllerStyle.Alert);
                    okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                    PresentViewController(okAlertController, true, null);
                }
                else
                {
                    try
                    {
                        var image = _signature.GetImage();
                        var points = _signature.Points;

                        /*
                        // save to App directory
                        var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        string jpgFilename = System.IO.Path.Combine(documentsDirectory, $"Signature{DateTime.Now.Ticks}.png");
                        var imgData = image.AsPNG();
                        if (imgData.Save(jpgFilename, false, out var err))
                        {
                            Console.WriteLine("saved as " + jpgFilename);
                        }
                        else
                        {
                            Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
                        }
                        */

                        // save to album
                        image.SaveToPhotosAlbum((img, error) =>
                        {
                            if (error != null)
                            {
                                Console.WriteLine("error saving image: {0}", error);
                            }
                            else
                            {
                                var okAlertController = UIAlertController.Create("OK Alert", "Signature saved.", UIAlertControllerStyle.Alert);
                                okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                                PresentViewController(okAlertController, true, null);
                            }
                        });
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }
                }

            };
            return btn;
        }
    }
}