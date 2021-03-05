using System;
using System.Windows;
using System.Windows.Controls;

namespace YoutubeBackingTracks
{
    /// <summary>
    /// Interaction logic for PageVideo.xaml
    /// </summary>
    public partial class PageVideo : Page
    {
        private readonly Frame MainFrame;
        private MainWindow window;
        private string VideoAddress;
        public PageVideo()
        {
            window = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();
            VideoAddress = window.address;          
        }
        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        { 
            if (webView != null )
            {
                webView.CoreWebView2.Navigate(VideoAddress);
            }
        }       

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (webView != null)
            {
                try
                {
                    await webView.EnsureCoreWebView2Async(null);
                    webView.Source = new Uri("https://www.youtube.com/embed/" + VideoAddress);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());

                }
            }
        }
    }
}

