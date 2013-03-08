using System;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Flashlight
{
    public partial class MainPage : PhoneApplicationPage
    {
        private VideoCamera _videoCamera;
        private VideoCameraVisualizer _videoCameraVisualizer;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            var uriSource = new Uri(@"/Flashlight;component/Images/On.png", UriKind.Relative);
            img.Source = new BitmapImage(uriSource);

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            try
            {
                // Use standard camera on back of device.
                _videoCamera = new VideoCamera();

                // Event is fired when the video camera object has been initialized.
                _videoCamera.Initialized += VideoCamera_Initialized;


                // Add the photo camera to the video source
                _videoCameraVisualizer = new VideoCameraVisualizer();
                _videoCameraVisualizer.SetSource(_videoCamera);
            }
            catch (Exception)
            {
                isInitialized = false;
                MessageBox.Show("your phone doesn't suport this app!");
            }


        }

        private void VideoCamera_Initialized(object sender, EventArgs e)
        {
            isInitialized = true;
        }

        bool isInitialized;
        bool isFlashEnabled;

        private void switchLight(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isInitialized)
            {


            }

            try
            {
                if (!isFlashEnabled)
                {
                    isFlashEnabled = true;

                    // Check to see if the camera is available on the device.
                    if (PhotoCamera.IsCameraTypeSupported(CameraType.Primary))
                    {
                        _videoCamera.LampEnabled = true;
                        bool v = _videoCamera.StartRecording();

                        if (v)
                        {
                            var uriSource = new Uri(@"/Flashlight;component/Images/OffLight.png", UriKind.Relative);
                            img.Source = new BitmapImage(uriSource);
                        }
                    }
                }
                else
                {
                    isFlashEnabled = false;
                    Boolean v = _videoCamera.StopRecording();
                    var uriSource = new Uri(@"/Flashlight;component/Images/On.png", UriKind.Relative);
                    img.Source = new BitmapImage(uriSource);

                }
            }
            catch (Exception)
            {
                isInitialized = false;
                MessageBox.Show("your phone doesn't suport this app!");
                

            }

        }

    }
}