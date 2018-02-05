using System;
using System.ComponentModel;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Test.Droid.Repos;
using Test.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ResolutionGroupName("Test")]
[assembly: ExportEffect(typeof(TapWithPositionGestureEffect), nameof(TapWithPositionGestureEffect))]

namespace Test.Droid.Repos
{
    public class TapWithPositionGestureEffect : PlatformEffect
    {
        private GestureDetectorCompat _gestureRecognizer;
        private readonly InternalGestureDetector _tapDetector;
        private Command<Point> _tapWithPositionCommand;
        private DisplayMetrics _displayMetrics;

        public TapWithPositionGestureEffect()
        {
            _tapDetector = new InternalGestureDetector
            {
                TapAction = motionEvent =>
                {
                    var tap = _tapWithPositionCommand;
                    if (tap != null)
                    {
                        var x = motionEvent.GetX();
                        var y = motionEvent.GetY();

                        var point = PxToDp(new Point(x, y));
                        Log.WriteLine(LogPriority.Debug, "gesture", $"Tap detected at {x} x {y} in forms: {point.X} x {point.Y}");
                        if (tap.CanExecute(point))
                            tap.Execute(point);
                    }
                }
            };
        }

        private Point PxToDp(Point point)
        {
            point.X = point.X / _displayMetrics.Density;
            point.Y = point.Y / _displayMetrics.Density;
            return point;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            _tapWithPositionCommand = Gesture.GetCommand(Element);
        }

        protected override void OnAttached()
        {
            var control = Control ?? Container;

            var context = control.Context;
            _displayMetrics = context.Resources.DisplayMetrics;
            _tapDetector.Density = _displayMetrics.Density;

            if (_gestureRecognizer == null)
                _gestureRecognizer = new GestureDetectorCompat(context, _tapDetector);
            control.Touch += ControlOnTouch;

            OnElementPropertyChanged(new PropertyChangedEventArgs(string.Empty));
        }

        private void ControlOnTouch(object sender, View.TouchEventArgs touchEventArgs)
        {
            _gestureRecognizer?.OnTouchEvent(touchEventArgs.Event);
        }

        protected override void OnDetached()
        {
            var control = Control ?? Container;
            control.Touch -= ControlOnTouch;
        }


        sealed class InternalGestureDetector : GestureDetector.SimpleOnGestureListener
        {
            public Action<MotionEvent> TapAction { private get; set; }
            public float Density { get; set; }

            public override bool OnSingleTapUp(MotionEvent e)
            {
                TapAction?.Invoke(e);
                return true;
            }
        }
    }
}