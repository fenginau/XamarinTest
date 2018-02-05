using System;
using System.Linq;
using Microsoft.AspNet.SignalR.Client;
using Test.Models;
using Test.Services;
using Test.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Chat : ContentPage
	{
	    private readonly HubProxy _hubProxy;
	    private readonly RestService _restService;
	    private int _convId;
	    private StackLayout _parent;
        public Chat (HubProxy hubProxy, RestService restService)
        {
            _parent = new StackLayout();
            _hubProxy = hubProxy;
		    _restService = restService;
		    _convId = -1;
            GetChats();
            InitializeComponent();
            ChatContent.Content = _parent;
            _hubProxy.ChatHubProxy.On<MessageModel>("newMessage", ReceiveChat);
        }

	    public async void GetChats()
	    {
	        var result = await _restService.GetChats(_convId);
            if (result.IsSuccess)
            {
                var conversation = (ConversationModel) result.Content;
                _convId = conversation.ConvId;
                conversation.Chats.ForEach(chat =>
                {
                    chat.IsMe = chat.User_Id.ToString() == "00000000-0000-0000-0000-000000000000";
                    _parent.Children.Add(GetMessageTemplate(chat));
                });
            }
        }

        public void SendMessage(object sender, EventArgs e)
	    {
	        Message.Focus();
	        var message = new MessageModel
	        {
	            Message = Message.Text,
                Name = "Test",
                Type = "text"
	        };
	        _hubProxy.SendMessage(message);
            var chat = new ChatModel
            {
                Create_Dt = DateTime.Now,
                Message = Message.Text,
                IsMe = true,
                Type = "text"
            };
	        Message.Text = "";
            _parent.Children.Add(GetMessageTemplate(chat));
	    }

	    public void ReceiveChat(MessageModel message)
	    {
            Console.WriteLine("Fired 123");
	        var chat = new ChatModel
	        {
	            Create_Dt = DateTime.Now,
	            IsMe = false,
	            Message = message.Message
	        };
	        Console.WriteLine(_parent.Children.Count);
	        var chatbox = GetMessageTemplate(chat);

	        Device.BeginInvokeOnMainThread(() =>
	        {
	            _parent.Children.Add(chatbox);
	        });
	    }

        private StackLayout GetMessageTemplate(ChatModel chat)
	    {
            var grid = new Grid
	        {
	            ColumnDefinitions =
	            {
	                new ColumnDefinition {Width = new GridLength(chat.IsMe ? 10 : 90, GridUnitType.Star)},
	                new ColumnDefinition {Width = new GridLength(chat.IsMe ? 90 : 10, GridUnitType.Star)},
	            },
	            ColumnSpacing = 0,
	            RowSpacing = 0
	        };
	        var chatFrame = new Frame
	        {
	            CornerRadius = 10,
                HorizontalOptions = chat.IsMe ? LayoutOptions.EndAndExpand : LayoutOptions.StartAndExpand,
                Margin = new Thickness(16, 5, 16, 5),
                BackgroundColor = chat.IsMe ? ColorHelper.GetMainColor() : Color.White,
                Content = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Children = { new Label { Text = chat.Message } }
                }
            };
	        grid.Children.Add(chatFrame, chat.IsMe ? 1 : 0, 0);
	        grid.Children.Add(new StackLayout { BackgroundColor = Color.Transparent}, chat.IsMe ? 0 : 1, 0);


            var msgContainer = new StackLayout
	        {
	            Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    grid
                }
	        };
	        return msgContainer;
	    }

	    public void ScrollToBottom()
	    {
	        var lastChild = ChatContent.Children.LastOrDefault();
	        ChatContent.ScrollToAsync(lastChild, ScrollToPosition.End, true);
	    }

	    public void EntryFocusHandler(object sender, FocusEventArgs e)
	    {
	        ScrollToBottom();
	    }
    }
}