using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace YoutubeBackingTracks
{
    /// <summary>
    /// Interaction logic for PageEmpty.xaml
    /// </summary>
    public partial class PageEmpty : Page
    {
        NavigationService navigationService;

        public PageEmpty()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Just an empty page to "reload" pageVideo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navigationService = this.NavigationService;            
            navigationService.Navigate(new Uri("PageVideo.xaml", UriKind.Relative));
        }
    }
}
