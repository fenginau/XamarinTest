using System;
using Xamarin.Forms;

namespace Test.Droid.Repos
{
    public class ChromeClient : Android.Webkit.WebChromeClient
    {
        public override void OnPermissionRequest(Android.Webkit.PermissionRequest request)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                request.Grant(request.GetResources());
            });
        }
    }
}
