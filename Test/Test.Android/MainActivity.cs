using System;
using Android;
using Android.App;
using Android.Content.PM;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Android.OS;

namespace Test.Droid
{
	[Activity (Label = "Test", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
        protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

		    string[] permissions =
		    {
		        Manifest.Permission.Camera,
		        Manifest.Permission.WriteExternalStorage,
                Manifest.Permission.ReadExternalStorage,
                Manifest.Permission.Internet,
		        Manifest.Permission.ModifyAudioSettings,
		        Manifest.Permission.RecordAudio,
		        Manifest.Permission.Flashlight
            };

		    RequestPermissions(permissions, 0);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            LoadApplication (new Test.App ());
        }
	}
}

