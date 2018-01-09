using System;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Providers
{
    public interface IHttpClientProvider
    {
        IHttpClientProvider CreateClient(Uri url);

        IHttpClientProvider WithBasicAuthenticator(string user, string password);

        IHttpClientProvider WithRequest(string request);

        Task<T> GetAsync<T>() where T : class;
    }
}
