using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Providers.Impl
{
    public class RestSharpHttpClientProvider : IHttpClientProvider
    {

        private RestClient _client;
        private RestRequest _request;


        public IHttpClientProvider CreateClient(Uri uri)
        {
            _client = new RestClient(uri.AbsoluteUri);
            return this;
        }


        public IHttpClientProvider WithBasicAuthenticator(string user, string password)
        {
            _client.Authenticator = new HttpBasicAuthenticator("username", "foo", "password", "bar");
            _client.AddDefaultHeader("Authorization", EncodeTo64("techuser:secretpassWord"));
            return this;
        }


        public IHttpClientProvider WithRequest(string request)
        {
            _request = new RestRequest(request);
            return this;
        }


        public async Task<T> GetAsync<T>() where T : class
        {
            var cancellationTokenSource = new CancellationTokenSource();

            
            var restResponse = _client.Get(_request);

            return JsonConvert.DeserializeObject<T>(restResponse.Content);
        }


        private string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;
        }
    }
}
