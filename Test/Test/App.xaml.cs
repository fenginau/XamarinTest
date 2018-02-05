using Test.Services;
using Test.Views;
using Xamarin.Forms;

namespace Test
{
	public partial class App : Application
	{
        public HubProxy HubProxy;

        public App ()
		{
			InitializeComponent();
		    HubProxy = new HubProxy();
		    var restService = new RestService();
            MainPage = new NavigationPage(new MainPage(HubProxy, restService)) { Title = "Main Page" };
		}

		protected override void OnStart ()
		{
		    HubProxy.StartConnectionAsync();
		}

		protected override void OnSleep ()
		{
		    HubProxy.StopConnection();
            // Handle when your app sleeps
        }

		protected override void OnResume ()
		{
		    HubProxy.StartConnectionAsync();
		    // Handle when your app resumes
		}
	}
}
