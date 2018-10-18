using Newtonsoft.Json;
using SpotifyFinder.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyFinder.Data
{
    class AccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
    }
    public class SpotifyToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
    public class HttpGrabber
    {
        private string BaseAddress = "https://accounts.spotify.com/authorize/";

        internal static ObservableCollection<Item> SearchList = new ObservableCollection<Item>();

        public async Task MakeStringGreatAgain(string search)
        {
            AlbumsList root = new AlbumsList();
            string url = string.Format("https://api.spotify.com/v1/search?q=" + search +"&type=album");
            //var getAuth = await GetAccessToken();
            var token = await GetToken();
            root = JsonConvert.DeserializeObject<AlbumsList>(await GetSpotifyType(token.access_token, url));
            try
            {
                SearchList.ToList().All(p => SearchList.Remove(p));
                foreach (var item in root.albums.items) SearchList.Add(item);
            }
            catch (Exception ex)
            {

            }


        }
        public async Task<SingleAlbum> GetSingleAlbum(string albumId)
        {
            SingleAlbum root = new SingleAlbum();
            string url = string.Format("https://api.spotify.com/v1/albums/" + albumId + "? market = PL");
            //var getAuth = await GetAccessToken();
            var token = await GetToken();
            root = JsonConvert.DeserializeObject<SingleAlbum>(await GetSpotifyType(token.access_token, url));
            try
            {
                //SearchList.ToList().All(p => SearchList.Remove(p));
                //foreach (var item in root.available_markets=) SearchList.Add(item);
            }
            catch (Exception ex)
            {

            }
            return root;

        }
        //     private async Task<string> GetAccessToken()
        //     {
        //     SpotifyToken token = new SpotifyToken();

        //string postString = string.Format("grant_type=client_credentials");
        //         byte[] byteArray = Encoding.UTF8.GetBytes(postString);

        //         string url = "https://accounts.spotify.com/api/token";

        //         WebRequest request = WebRequest.Create(url);
        //         request.Method = "POST";
        //         request.Headers.Add("Authorization", "Basic {Encoded 53fe82d95b9542068c866fafbc769a8c:cc58a24a9312406090e328fcdca58c1b}");
        //         request.ContentType = "application/x-www-form-urlencoded";
        //         request.ContentLength = byteArray.Length;
        //         using (Stream dataStream = request.GetRequestStream())
        //         {
        //             dataStream.Write(byteArray, 0, byteArray.Length);
        //             using (WebResponse response = await request.GetResponseAsync())
        //             {
        //                 using (Stream responseStream = response.GetResponseStream())
        //                 {
        //                     using (StreamReader reader = new StreamReader(responseStream))
        //                     {
        //                         string responseFromServer = reader.ReadToEnd();
        //                         token = JsonConvert.DeserializeObject<SpotifyToken>(responseFromServer);
        //                     }
        //                 }
        //             }
        //         }
        //         return token.access_token;
        //     }
        static async Task<AccessToken> GetToken()
        {
            Console.WriteLine("Getting Token");
            string clientId = "53fe82d95b9542068c866fafbc769a8c";
            string clientSecret = "cc58a24a9312406090e328fcdca58c1b";
            string credentials = String.Format("{0}:{1}", clientId, clientSecret);

            using (var client = new HttpClient())
            {
                //Define Headers
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                //Prepare Request Body
                List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                //Request Token
                var request = await client.PostAsync("https://accounts.spotify.com/api/token", requestBody);
                var response = await request.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AccessToken>(response);
            }
        }
        private async Task<string> GetSpotifyType(string token, string url)
        {
            //try
            //{
                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";
                request.Headers.Add("Authorization", "Bearer " + token);
                request.ContentType = "application/json; charset=utf-8";
            //T type = default(T);
            string responseFromServer = " ";

                using (WebResponse response = await request.GetResponseAsync())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            responseFromServer = reader.ReadToEnd();
                            //type = JsonConvert.DeserializeObject<T>(responseFromServer);
                        }
                    }
                }

                return responseFromServer;
            //}
            //catch (Exception ex)
            //{
            //    //Console.WriteLine("Error", ex);
            //    throw;
            //}
        }


    }
}
