using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace OpLab.Web.Helpers
{
    public static class ApiClient
    {
        public static HttpClient GetClient()
        {
            var client = new HttpClient { BaseAddress = new Uri("http://localhost:33224/") };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
