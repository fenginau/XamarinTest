using System;
using Test.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Record : ContentPage
	{
	    private int _number;
	    public Record ()
		{
		    Title = "Record";
		    _number = 0;
            TapDel tapHandler = OnGetPoint;

            var recordModel = new RecordViewModel(tapHandler);
            BindingContext = recordModel;
            InitializeComponent();
		}

	    public delegate void TapDel(Point p);

	    public void OnGetPoint(Point p)
	    {
	        DrawCircle(p.X, p.Y, _number);
        }

        public void DrawCircle(double positionX, double positionY, int number)
        {
            _number++;
            var circleBtn = new Button
	        {
                HeightRequest = 40,
                WidthRequest = 40,
                BorderRadius = 20,
                BorderColor = Color.Red,
                BorderWidth = 2,
                BackgroundColor = Color.Transparent
            };

            ImgContainer.Children.Add(circleBtn,
                Constraint.RelativeToView(AssessmentImg, (parent, sibling) => sibling.X + positionX - 20),
                Constraint.RelativeToView(AssessmentImg, (parent, sibling) => sibling.Y + positionY - 20));

            circleBtn.Clicked += delegate
	        {
	            DisplayAlert("Review", "You clicked wound " + number, "OK");
	        };

	    }

    }
}