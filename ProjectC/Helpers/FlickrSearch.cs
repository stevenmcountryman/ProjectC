using FlickrNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Data.Json;

namespace ProjectC.Helpers
{
    public class FlickrSearch
    {
        private static string API_KEY = "0eccbf499fc77902bc2a5432e64325c7";
        private static string APP_SECRET = "d7dcaaaf6b6acd66";
        private Flickr flickr;
        
        public FlickrSearch()
        {
            flickr = new Flickr(API_KEY);
        }

        public async Task<PhotoCollection> SearchImages(string query)
        {
            var options = new PhotoSearchOptions { PerPage = 20, Page = 1 , Text = query, SortOrder = PhotoSearchSortOrder.Relevance};
            PhotoCollection photos = await flickr.PhotosSearchAsync(options);
            return photos;
        }
    }
}
