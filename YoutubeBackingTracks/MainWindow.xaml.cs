using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Web.WebView2.Core;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using YoutubeBackingTracks;
using Microsoft.Web.WebView2.Core;
using System.IO;

namespace YoutubeBackingTracks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string address;
        public DataTable dt;
        public PageVideo PageVideo { get; set; }
        public PageEmpty PageEmpty { get; set; }
        List<TextBox> LstTb = new List<TextBox>();
        string dataFile;
        // DataContext = dt.AsDataView();
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

            // MainFrame.Navigate(new Uri("PageVideo.xaml", UriKind.Relative));
        }

        private void CreateFilename()
        {
            string subPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string dirName = @"\BackingTracks";
            bool exists = System.IO.Directory.Exists(subPath + dirName);
            if (!exists) System.IO.Directory.CreateDirectory(subPath + dirName);
             dataFile = subPath + dirName +  @"\BackingTraks.xml";
        }

        private void CreateXml()
        {

           
            dt.WriteXml(dataFile);

        }

        // <summary>
        /// method for reading an XML file into a DataTable
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

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool update = true;
            BackingTracks bt = new BackingTracks();
            bt.Artist = TbxArtist.Text;
            bt.Song = TbxSong.Text;
            bt.Url = TbxUrl.Text;
            foreach (var item in LstTb)
            {
                if (string.IsNullOrEmpty(item.Text))
                {
                    update = false;
                }
            }

            if (update)
            {
                dt.Rows.Add(bt.Id(), bt.Artist, bt.Song, bt.Url);
                DgYtb.ItemsSource = null;
                DgYtb.ItemsSource = dt.DefaultView;
                dt.WriteXml(dataFile);
            }           
        }

        private void DgYtb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string youtubeLink = (DgYtb.SelectedItem as DataRowView).Row[3].ToString();
            address = youtubeLink.UrlToEmbedCode();
            //address = (DgYtb.SelectedItem as DataRowView).Row[3].ToString();
            //string youtubeLink = address;
            //address =(DgYtb.SelectedItem as DataRowView).Row[3].ToString();

            MainFrame.Navigate(new Uri("PageEmpty.xaml", UriKind.Relative)); 
        }
    }
}
