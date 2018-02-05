using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Test.Controls
{
    public class VideoView : View
    {
        public Action StopAction;
        public VideoView()
        {

        }

        public static readonly BindableProperty FileSourceProperty =
            BindableProperty.Create(nameof(FileSource), typeof(string), typeof(VideoView), null);

        public string FileSource
        {
            get => (string)GetValue(FileSourceProperty);
            set => SetValue(FileSourceProperty, value);
        }
        
    }
}
