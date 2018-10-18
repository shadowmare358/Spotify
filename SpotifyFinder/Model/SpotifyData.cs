using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyFinder.Model
{
    public class SpotifyData
    {
        public string id { get; set; }
        public string name { get; set; }

    }
    public class ExternalUrls
    {
        public string spotify { get; set; }
    }

    public class Artist
    {
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Image
    {
        public int? height { get; set; }
        public string url { get; set; }
        public int? width { get; set; }
    }

    public class ExternalUrls2
    {
        public string spotify { get; set; }
    }

    public class Owner
    {
        public string display_name { get; set; }
        public ExternalUrls2 external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }


    public class Item
    {
        public int disc_number { get; set; }
        public int duration_ms { get; set; }
        public string href { get; set; }
        
        public string album_type { get; set; }
        public List<Artist> artists { get; set; }
        public List<string> available_markets { get; set; }
        public ExternalUrls2 external_urls { get; set; }
        public string preview_url { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public string name { get; set; }

        public string uri { get; set; }

    }

    public class Playlists
    {
        //public string href { get; set; }
        public List<Item> items { get; set; }
        //public int limit { get; set; }
        //public string next { get; set; }
        //public int offset { get; set; }
        //public object previous { get; set; }
        //public int total { get; set; }
    }
    public class Albums
    {
        public string href { get; set; }
        public List<Item> items { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public  int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }
    }
    public class AlbumsList
    {
        public Albums albums { get; set; }
    }
    public class SingleAlbum
    {
        public string album_type { get; set; }
        public List<Artist> artists { get; set; }
        public List<string> available_markets { get; set; }
       // public List<Copyright> copyrights { get; set; }
        //public ExternalIds external_ids { get; set; }
        public ExternalUrls2 external_urls { get; set; }
        public List<object> genres { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string release_date { get; set; }
        public string release_date_precision { get; set; }
        public Tracks tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }

    }
    public class Tracks
    {
        public string href { get; set; }
        public List<Item> items { get; set; }
       
        public int total { get; set; }
    }
}
