using System;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Providers
{
    public interface IHttpClientProvider
    {
        IHttpClientProvider CreateClient(Uri url);

        IHttpClientProvider WithBasicAuthenticator(string user, string password);

        Task<T> GetAsync<T>(string request) where T : class;
    }
}
