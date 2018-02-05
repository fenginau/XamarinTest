using System;
using System.ComponentModel;
using Test.iOS.Repos;
using Test.Utils;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Test")]
[assembly: ExportEffect(typeof(TapWithPositionGestureEffect), nameof(TapWithPositionGestureEffect))]
namespace Test.iOS.Repos
{
    internal class TapWithPositionGestureEffect : PlatformEffect
    {
        private readonly UITapGestureRecognizer _tapDetector;
        private Command<Point> _tapWithPositionCommand;

        public TapWithPositionGestureEffect()
        {
            _tapDetector = CreateTapRecognizer(() => _tapWithPositionCommand); ;
        }

        private UITapGestureRecognizer CreateTapRecognizer(Func<Command<Point>> getCommand)
        {
            return new UITapGestureRecognizer(() =>
            {
                var handler = getCommand();
                if (handler != null)
                {
                    var control = Control ?? Container;
                    var tapPoint = _tapDetector.LocationInView(control);
                    var point = new Point(tapPoint.X, tapPoint.Y);

                    if (handler.CanExecute(point) == true)
                        handler.Execute(point);
                }
            })
            {
                Enabled = false,
                ShouldRecognizeSimultaneously = (recognizer, gestureRecognizer) => true,
            };
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            _tapWithPositionCommand = Gesture.GetCommand(Element);
        }

        protected override void OnAttached()
        {
            var control = Control ?? Container;

            control.AddGestureRecognizer(_tapDetector);
            control.UserInteractionEnabled = true;
            _tapDetector.Enabled = true;

            OnElementPropertyChanged(new PropertyChangedEventArgs(String.Empty));
        }

        protected override void OnDetached()
        {
            var control = Control ?? Container;
            _tapDetector.Enabled = false;
            control.RemoveGestureRecognizer(_tapDetector);
        }
    }
}