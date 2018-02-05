using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Photo : ContentPage
	{
	    private int _currentPhoto;
	    private List<string> _files;
	    private string _imgDir;
        public Photo ()
		{
		    Title = "Photo";
		    SetImgDir();
			InitializeComponent();
        }

	    public void SetImgDir()
	    {
	        var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
	        var directoryName = Path.Combine(documents, GlobalVariable.ImageDir);
	        if (!Directory.Exists(directoryName))
	        {
	            Directory.CreateDirectory(directoryName);
	        }
	        _imgDir = directoryName;
	        _files = Directory.EnumerateFiles(_imgDir).ToList();
        }

	    public async void TakePhoto(object sender, EventArgs e)
	    {
	        try
	        {
	            await CrossMedia.Current.Initialize();

	            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
	            {
	                await DisplayAlert("No Camera", ":( No camera available.", "OK");
	                return;
	            }

	            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
	            {
	                Directory = "Sample",
	                Name = "test.jpg",
	                SaveToAlbum = true,
	                AllowCropping = true,
	                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear
	            });
	            if (file != null)
	            {
	                var stream = file.GetStream();
	                using (MemoryStream ms = new MemoryStream())
	                {
	                    stream.CopyTo(ms);
	                    byte[] imageBytes = ms.ToArray();
	                    string base64String = Convert.ToBase64String(imageBytes);
	                    SaveStringToFile(base64String);
	                }
	                stream.Dispose();
	                file.Dispose();
	            }
            }
	        catch (Exception exception)
	        {
	            Console.WriteLine(exception);
	        }
        }

	    public void SaveStringToFile(string content)
	    {
	        try
	        {
	            var filename = Path.Combine(_imgDir, $"Image{DateTime.Now.Ticks}.txt");
	            File.WriteAllText(filename, content);
	            _files.Add(filename);
	            _currentPhoto = _files.Count - 1;
                GetPhoto(filename);
            }
	        catch (Exception e)
	        {
	            Console.WriteLine(e);
	        }
        }

	    public void GetPhoto(string path)
	    {
	        var content = File.ReadAllText(path);
	        SetImgFromBase64(content);
        }

	    public void SetImgFromBase64(string baseString)
	    {
	        var imgByte = Convert.FromBase64String(baseString);
	        Viewer.Source = ImageSource.FromStream(() => new MemoryStream(imgByte));
        }

	    public void LastPhoto(object sender, EventArgs e)
	    {
	        if (_currentPhoto > 1)
	        {
	            GetPhoto(_files[--_currentPhoto]);
	        }
	    }

	    public void NextPhoto(object sender, EventArgs e)
	    {
	        if (_currentPhoto < _files.Count - 1)
	        {
	            GetPhoto(_files[++_currentPhoto]);
	        }
        }
    }
}