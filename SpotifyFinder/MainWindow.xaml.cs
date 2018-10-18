using SpotifyFinder.Data;
using SpotifyFinder.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace SpotifyFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpGrabber http = new HttpGrabber();
        internal static Frame _mainFrame;
        AlbumListPage albumListPage;
        AlbumDetailsPage albumDetailsPage;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void InitProgram()
        {
            _mainFrame = MainFrame;
            albumListPage = new AlbumListPage(http);
           
            _mainFrame.Navigate(albumListPage); 
        }
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            InitProgram();
        }

        private async Task GetData(string search)
        {
            await http.MakeStringGreatAgain(search);
           

            //DataList.ItemsSource = data.playlists.items;
        }

        private void searchButtonOnClick(object sender, RoutedEventArgs e)
        {
            string search = searchBox.Text;

            GetData(search);
        }


    }
}
