using System.IO;
using Xamarin.Forms.Platform.Android;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Test.Droid.Renderer;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Test.Controls.VideoView), typeof(VideoViewRenderer))]
namespace Test.Droid.Renderer
{
    public class VideoViewRenderer : ViewRenderer<Controls.VideoView, Android.Widget.VideoView>, ISurfaceHolderCallback
    {
        Android.Widget.VideoView _videoview;
        MediaPlayer _player;
        public VideoViewRenderer()
        {
        }

        public void SurfaceChanged(ISurfaceHolder holder, global::Android.Graphics.Format format, int width, int height)
        {

        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Test.Controls.VideoView> e)
        {
            base.OnElementChanged(e);
            e.NewElement.StopAction = () => {
                this._player.Stop();
                //this.Control.StopPlayback();
            };

            _videoview = new Android.Widget.VideoView(Context);

            base.SetNativeControl(_videoview);
            Control.Holder.AddCallback(this);
            _player = new MediaPlayer();
            Play(e.NewElement.FileSource);

        }
        void Play(string fullPath)
        {
            //var folder = "/Download/";
            //var path = Path.Combine(folder.ToString(), fullPath);
            //System.Console.WriteLine(fullPath);
            //AssetFileDescriptor afd = Forms.Context.Assets.OpenFd(path);
            //CameraCapturer cameraCapturer = new CameraCapturer(Android.App.Application.Context, CameraCapturer.CameraSource.FrontCamera);
            //LocalVideoTrack localVideoTrack = LocalVideoTrack.Create(Android.App.Application.Context, true, cameraCapturer);

            //localVideoTrack.AddRenderer(_videoview);
            //if (afd != null)
            //{
            //_player.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
            //_player.Prepare();
            //_player.Start();
            //Control.Layout(0, 200, _player.VideoHeight, _player.VideoWidth);

            //}

        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            _player.SetDisplay(holder);
        }
    }
}