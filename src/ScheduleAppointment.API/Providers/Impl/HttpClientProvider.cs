using Conditions;
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

        private ILoggerProvider _loggerProvider;


        public HttpClientProvider(ILoggerProvider loggerProvider)
        {
            _loggerProvider = loggerProvider;
        }


        public IHttpClientProvider CreateClient(string uri)
        {
            try
            {
                Condition.Requires(uri)
                        .IsNotNullOrEmpty("Uri for create http client is mandatory")
                        .Evaluate(Uri.TryCreate(uri, UriKind.Absolute, out Uri uriResult), "The string uri is not valid");

                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };

                _client = new HttpClient(handler){ BaseAddress = new Uri(uri) };
                
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return this;
            }
            catch (Exception err)
            {
                _loggerProvider.Log(err);
                throw (err);
            }
        }


        public IHttpClientProvider WithBasicAuthenticator(string user, string password)
        {
            try
            {
                Condition.Requires(user)
                    .IsNotNullOrEmpty("user name for basic authentication is mandatory");

                Condition.Requires(password)
                    .IsNotNullOrEmpty("password for basic authentication is mandatory");

                var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", user, password));
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                return this;
            }
            catch (Exception err)
            {
                _loggerProvider.Log(err);
                throw (err);
            }
        }


        public async Task<string> GetStringAsync(string request)
        {
            try
            {
                Condition.Requires(request)
                    .IsNotNullOrEmpty("request method for httpClient is mandatory");

                using (_client)
                {
                    return await _client.GetStringAsync(request);
                }
            }
            catch (Exception err)
            {
                await _loggerProvider.Log(err);
                throw (err);
            }
        }
    }
}
