using ProjectC.Controls;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Devices.Input;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ProjectC
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ImageCanvas_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private async void ImageCanvas_Drop(object sender, DragEventArgs e)
        {
            var dropPoint = e.GetPosition(this.DrawingBoard);
            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                var URL = await e.DataView.GetTextAsync();

                var bitmapImage = new BitmapImage(new Uri(URL));
                Image image = new Image()
                {
                    Width = 200,
                    Height = 200,
                    Stretch = Stretch.UniformToFill,
                    Source = bitmapImage                    
                };
                image.PointerPressed += Image_PointerPressed;
                image.PointerReleased += Image_PointerReleased;
                image.PointerMoved += Image_PointerMoved;

                this.DrawingBoard.Children.Add(image);
                Canvas.SetLeft(image, dropPoint.X - image.Width / 2);
                Canvas.SetTop(image, dropPoint.Y - image.Height / 2);
            }

        }

        private void Image_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var image = sender as Image;
            if (image.PointerCaptures != null && image.PointerCaptures.Count > 0)
            {
                if (image.PointerCaptures[0].PointerId == e.Pointer.PointerId)
                {
                    var X = e.GetCurrentPoint(this.DrawingBoard).Position.X - (image.Width / 2);
                    var Y = e.GetCurrentPoint(this.DrawingBoard).Position.Y - (image.Height / 2);

                    if (X >= 0 && X <= this.DrawingBoard.ActualWidth)
                    {
                        Canvas.SetLeft(image, X );
                    }
                    if (Y >= 0 && Y <= this.DrawingBoard.ActualHeight)
                    {
                        Canvas.SetTop(image, Y);
                    }
                }
            }
        }

        private void Image_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var image = sender as Image;
            image.ReleasePointerCapture(e.Pointer);
        }

        private void Image_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse ||
                e.Pointer.PointerDeviceType == PointerDeviceType.Touch)
            {
                var image = sender as Image;
                image.CapturePointer(e.Pointer);
            }
        }

        private void BackgroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            var oldBackColor = this.DrawingBoard.Background;
            this.DrawingBoard.Background = this.BackgroundColorButton.Fill;
            this.BackgroundColorButton.Fill = oldBackColor;
        }
    }
}
