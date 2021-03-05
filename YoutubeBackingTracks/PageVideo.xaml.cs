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
            //if (webView != null)
            //{
            //    webView.CoreWebView2.Navigate(VideoAddress);
            //}
        }
        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
           //r txt = addressBar.Text;

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
                    webView.CoreWebView2.Navigate(VideoAddress);
                }
                catch (Exception)
                {
                    string html = "<html><head>";
                    html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
                    html += "<iframe id='video' src= 'https://www.youtube.com/embed/{0}' width='600' height='300' frameborder='0' allowfullscreen></iframe>";
                    html += "</body></html>";
                    webView.Source = new Uri("https://www.youtube.com/embed/" + VideoAddress);

                }
                //await webView.EnsureCoreWebView2Async(null);
               // webView.CoreWebView2.Navigate(VideoAddress);
                
            }
        }
    }
}

