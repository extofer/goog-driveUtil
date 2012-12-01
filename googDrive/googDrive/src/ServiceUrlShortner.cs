using System;
using Google.Apis.Urlshortener.v1;
using Google.Apis.Urlshortener.v1.Data;

namespace goog.driveUtil
{
    public class ServiceUrlShortner : IServiceUrlShortner
    {
        private UrlshortenerService _urlshortenerService;

        public ServiceUrlShortner(UrlshortenerService urlshortenerService)
        {
            _urlshortenerService = urlshortenerService;
        }

        public string Shorten(string url)
        {
            try
            {
                var service = _urlshortenerService;

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