using FlickrNet;
using ProjectC.Helpers;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProjectC.Controls
{
    public sealed partial class WebSearch : Page
    {
        public WebSearch()
        {
            this.InitializeComponent();
            this.SearchButton.Focus(FocusState.Programmatic);
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            FlickrSearch imageSearch = new FlickrSearch();
            var photos = await imageSearch.SearchImages(this.SearchBox.Text.Replace(" ", "+"));
            this.PhotoGrid.ItemsSource = photos;
        }

        private void PhotoGrid_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            Photo photo = e.Items[0] as Photo;
            e.Data.SetText(photo.Medium800Url);
            e.Data.RequestedOperation = DataPackageOperation.Copy;
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.SearchText.Visibility = Visibility.Collapsed;
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.SearchBox.Text.Length == 0)
            {
                this.SearchText.Visibility = Visibility.Visible;
            }
        }
    }
}
