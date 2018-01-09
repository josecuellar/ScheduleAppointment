using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Providers.Impl
{
    public class HttpClientProvider : IHttpClientProvider
    {
        private HttpClient _client;

        public IHttpClientProvider CreateClient(Uri uri)
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            _client = new HttpClient(handler)
            {
                BaseAddress = uri
            };
            ;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return this;
        }


        public IHttpClientProvider WithBasicAuthenticator(string user, string password)
        {
            var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", user, password));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            return this;
        }


        public async Task<T> GetAsync<T>(string request) where T : class
        {
            using (_client)
            {
                var response = await _client.GetAsync(request);
                string stringData = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(stringData);
            }
        }
    }
}
