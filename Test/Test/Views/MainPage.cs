using Test.Services;
using Xamarin.Forms;

namespace Test.Views
{
	public class MainPage : TabbedPage
    {
		public MainPage (HubProxy hubProxy, RestService restService)
		{
		    Children.Add(new Record());
		    Children.Add(new Photo());
		    Children.Add(new Home(restService));
            Children.Add(new Chat(hubProxy, restService));
		    Children.Add(new Video());
        }
    }
}