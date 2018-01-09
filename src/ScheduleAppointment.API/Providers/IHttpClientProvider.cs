using System;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Providers
{
    public interface IHttpClientProvider
    {
        IHttpClientProvider CreateClient(string url);

        IHttpClientProvider WithBasicAuthenticator(string user, string password);

        Task<string> GetStringAsync(string request);
    }
}
