using NAudio.Wave;
using SpotifyFinder.Data;
using SpotifyFinder.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpotifyFinder.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy AlbumDetailsPage.xaml
    /// </summary>
    public partial class AlbumDetailsPage : Page
    {
        Thread playThread;
       private Item albumItem;
        private HttpGrabber http;
        internal static ObservableCollection<Item> SearchList = new ObservableCollection<Item>();
        public AlbumDetailsPage(Item item, HttpGrabber http)
        {
            InitializeComponent();
           
            this.albumItem = item;
            this.http = http;
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            GetData(albumItem.id);

        }
        bool waiting = false;
        AutoResetEvent stop = new AutoResetEvent(false);
        public void PlayMp3FromUrl(string url, int timeout)
        {
            using (Stream ms = new MemoryStream())
            {
                using (Stream stream = WebRequest.Create(url)
                    .GetResponse().GetResponseStream())
                {
                    byte[] buffer = new byte[32768];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }
                ms.Position = 0;
                using (WaveStream blockAlignedStream =
                    new BlockAlignReductionStream(
                        WaveFormatConversionStream.CreatePcmStream(
                            new Mp3FileReader(ms))))
                {
                    using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                    {
                        waveOut.Init(blockAlignedStream);
                        waveOut.PlaybackStopped += (sender, e) =>
                        {
                            waveOut.Stop();
                        };
                        waveOut.Play();
                        waiting = true;
                        stop.WaitOne(timeout);
                        waiting = false;
                    }
                }
            }
        }
        private void SearchList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void OnLeftMouse(object sender, MouseButtonEventArgs e)
        {

        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow._mainFrame.GoBack();
        }
        
        private async Task<SingleAlbum> GetData(string albumId)
        {
            var data = await http.GetSingleAlbum(albumId);
            foreach (var track in data.tracks.items.ToArray())
            {
                Lista_utworow.Items.Add(track.name);
            }
            return data;
           



        }
        public async Task PlayMusic()
        {
            var wybrany = Lista_utworow.SelectedItem.ToString();
            var data = await GetData(albumItem.id);
            foreach (var element in data.tracks.items.ToList())
            {
                if (element.name == wybrany)
                {
                    if(element.preview_url != null)
                    {
                        wybrany = element.preview_url;
                        playThread = new Thread(() => PlayMp3FromUrl(new Uri(wybrany).ToString(), 10000));
                        playThread.Start();
                    }
                    else
                    {
                        MessageBox.Show("No preview URL!");
                    }
                }

            }
           




        }
        
        //foreach (var item in data.tracks.items) SearchList.Add(item);
        //foreach (var track in SearchList)
        //{
        //    Lista_utworow.Items.Add(track.name);
        //}



        private void Button_Click(object sender, RoutedEventArgs e)
        {
             MainWindow._mainFrame.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            

               

           
            //Lista_utworow.Items.Add(dane);
            //var dane = GetData(albumItem.id);

           // var playThread = new Thread(() => PlayMp3FromUrl(new Uri(dane.Result.tracks.items.FirstOrDefault().preview_url).ToString(),10000));
           // playThread.IsBackground = true;
           ///playThread.Start();
        }


        private void Lista_utworow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var wybrany = Lista_utworow.SelectedItem.ToString();
            PlayMusic();
 



        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
