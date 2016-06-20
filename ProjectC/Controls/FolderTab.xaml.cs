using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace ProjectC.Controls
{
    public sealed partial class FolderTab : UserControl
    {
        public FolderTab()
        {
            this.InitializeComponent();
        }

        public Brush TabColor
        {
            get
            {
                return this.Tab.Fill;
            }
            set
            {
                this.Tab.Fill = value;
            }
        }

        public string IconSymbol
        {
            get
            {
                return this.Icon.Text;
            }
            set
            {
                this.Icon.Text = value;
            }
        }

        private void FolderTab_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            this.SlideAnimation.To = 12;
            this.SlideStory.Begin();
        }

        private void FolderTab_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            this.SlideAnimation.To = 0;
            this.SlideStory.Begin();
        }
    }
}
