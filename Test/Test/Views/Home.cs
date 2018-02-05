using SlideOverKit;
using System;
using Test.Controls;
using Test.Services;
using Xamarin.Forms;
using Test.Utils;
using ZXing.Net.Mobile.Forms;

namespace Test.Views
{
	public class Home : MenuContainerPage
	{
	    private readonly RestService _restService;
        public Home (RestService restService)
        {
            _restService = restService;
            Title = "Home";
            //var btn = new Button {Text = "send"};
		    //btn.Clicked += OnBtnClicked;


            Content = new StackLayout {
				Children = {
					new Label { Text = "Home Page" },
				    //btn,
                    new DrawView{ HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand }
                }
			};

		    ToolbarItems.Add(new ToolbarItem
		    {
		        Command = new Command(() => {
		            if (SlideMenu.IsShown)
		            {
		                HideMenuAction?.Invoke();
		            }
		            else
		            {
		                ShowMenuAction?.Invoke();
		            }
		        }),
		        Icon = "Settings.png",
		        Text = "Settings",
		        Priority = 0
		    });
            var a = new DateTime();
            var toolbarItem = new ToolbarItem { Text = "Scan", Priority = 0, Order = ToolbarItemOrder.Secondary };
            toolbarItem.Clicked += (sender, e) =>
            {
                ScanQrCode();
            };
            ToolbarItems.Add(toolbarItem);

            SlideMenu = new SlideDownMenuView();

        }

	    public async void ScanQrCode()
	    {
	        var scanPage = new ZXingScannerPage();

	        scanPage.OnScanResult += (result) => {
	            // Stop scanning
	            scanPage.IsScanning = false;

	            // Pop the page and show the result
	            Device.BeginInvokeOnMainThread(() => {
	                Navigation.PopAsync();
	                DisplayAlert("Scanned Barcode", result.Text, "OK");
	            });
	        };

	        // Navigate to our scanner page
	        await Navigation.PushAsync(scanPage);
        }

        //public void OnBtnClicked(object sender, EventArgs e)
        //{



        //    //await _restService.PostAsync();
        //}

    }
}