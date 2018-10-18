using SpotifyFinder.Data;
using SpotifyFinder.Model;
using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy AlbumListPage.xaml
    /// </summary>
    public partial class AlbumListPage : Page
    {
        private HttpGrabber http;
        public AlbumListPage()
        {
            InitializeComponent();
        }

        bool waiting = false;
        AutoResetEvent stop = new AutoResetEvent(false);
        
            //private void OnPageLoaded(object sender, RoutedEventArgs e )
            //{

            //    http.SearchList.CollectionChanged += SearchList_CollectionChanged;

            public AlbumListPage(HttpGrabber http)
        {
            InitializeComponent();
            this.http = http;
        }
        //}


        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            HttpGrabber.SearchList.CollectionChanged += SearchList_CollectionChanged;
            DataList.ItemsSource = HttpGrabber.SearchList;
        }

        private void SearchList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DataList.ItemsSource = HttpGrabber.SearchList;
        }


        private void DataList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {

                var item = (sender as ListView).SelectedItem as Item;
                MainWindow._mainFrame.Navigate(new AlbumDetailsPage(item, http));


            }
            catch (Exception)
            {

            }
        }
    }
}

