using System;
using Google.Apis.Urlshortener.v1;
using Google.Apis.Urlshortener.v1.Data;

namespace goog.DriveUtil
{
    public class ServiceUrlShortner
    {
        public string Shorten(string url)
        {
            try
            {
                var service = new UrlshortenerService();

                string urlToShorten = url;

                Url response = service.Url.Insert(new Url {LongUrl = urlToShorten}).Fetch();

                return response.Id;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}