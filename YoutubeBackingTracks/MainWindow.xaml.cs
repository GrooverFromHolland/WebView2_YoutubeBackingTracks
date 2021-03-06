using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace YoutubeBackingTracks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string address;
        public DataTable dt;
        List<TextBox> LstTb = new List<TextBox>();
        private string dataFile;

        public PageVideo PageVideo { get; set; }

        public PageEmpty PageEmpty { get; set; }

        public MainWindow()
        {            
            InitializeComponent();
            LstTb.Add(TbxArtist);
            LstTb.Add(TbxSong);
            LstTb.Add(TbxUrl);
            dt = new DataTable();

            PageVideo = new PageVideo();
            PageEmpty = new PageEmpty();
            dt.TableName = "YtFragments";
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Artist", typeof(string));
            dt.Columns.Add("Song", typeof(string));
            dt.Columns.Add("Url", typeof(string));
            BackingTracks bt = new BackingTracks();           
        }
        /// <summary>
        /// Create xml file location for creating datafile.
        /// </summary>
        private void CreateFilename()
        {
            string subPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string dirName = @"\BackingTracks";
            bool exists = System.IO.Directory.Exists(subPath + dirName);
            if (!exists) System.IO.Directory.CreateDirectory(subPath + dirName);
             dataFile = subPath + dirName +  @"\BackingTraks.xml";
        }

        /// <summary>
        /// Write Datatable to Xml file
        /// </summary>
        private void CreateXml()
        {           
            dt.WriteXml(dataFile);
        }

        // <summary>
        /// method for creating xml data file if not already exists.
        /// </summary>
        /// <param name="file">name (and path) of the XML file</param>
        /// <returns></returns>
        public DataTable ReadXML()
        {     
            dt.ReadXml(dataFile);
            if (dt == null)
            {
                CreateXml();
            }
            return dt;
        }



        public class BackingTracks
        {
            public string Artist { get; set; }

            public string Song { get; set; }

            public string Url { get; set; }

            public string Id()
            {
                return Artist + "/" + Song;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateFilename();
            dt = ReadXML();
            DgYtb.ItemsSource = dt.DefaultView;
        }

        /// <summary>
        /// Save input from text boxesand save to xml file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool update = true;
            BackingTracks bt = new BackingTracks();
            bt.Artist = TbxArtist.Text;
            bt.Song = TbxSong.Text;
            bt.Url = TbxUrl.Text;
            foreach (var item in LstTb)
            {
                //You cannot leave a TextBox empty.
                if (string.IsNullOrEmpty(item.Text))
                {
                    update = false;
                }
                item.Text = string.Empty;
            }

            if (update)
            {
                dt.Rows.Add(bt.Id(), bt.Artist, bt.Song, bt.Url);

                //refresh DgYtb.ItemsSource.
                DgYtb.ItemsSource = null;
                DgYtb.ItemsSource = dt.DefaultView;

                //save data.
                dt.WriteXml(dataFile);
            }           
        }
        /// <summary>
        /// If video is playing Close PageVideo and play new video in new PageVideo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgYtb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string youtubeLink = (DgYtb.SelectedItem as DataRowView).Row[3].ToString();
                if(DgYtb.SelectedItem == null)
                {
                    return;
                }

                // extract Youtube video Id to embed link
                address = youtubeLink.UrlToEmbedCode();

                // Force a reload of PageVideo from PageEmpy.
                MainFrame.Navigate(new Uri("PageEmpty.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
         
        }

        private void BtnHide_Click(object sender, RoutedEventArgs e)
        {
            if(StpnlTop.Visibility == Visibility.Visible)
            {
                StpnlTop.Visibility = Visibility.Collapsed;
                BtnHide.Content = "6";
                PageVideo.webView.HorizontalAlignment = HorizontalAlignment.Stretch;
                PageVideo.webView.VerticalAlignment= VerticalAlignment.Stretch;
            }
            else
            {
                StpnlTop.Visibility = Visibility.Visible;
                BtnHide.Content = "5";
                PageVideo.webView.HorizontalAlignment = HorizontalAlignment.Left;
                PageVideo.webView.VerticalAlignment = VerticalAlignment.Top;
            }
        }
    }
}
