using Microsoft.Extensions.Options;
using ScheduleAppointment.API.Model.DTO;
using ScheduleAppointment.API.Providers;
using ScheduleAppointment.API.Settings;
using System;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Services.Impl
{
    public class APIAvailabilityWeekService : IAvailabilityWeekService
    {

        private IHttpClientProvider _httpClientProvider;

        private readonly IOptions<APIGetAvailabilityWeekSettings> _serviceSettings;

        public APIAvailabilityWeekService(IHttpClientProvider httpClientProvider, IOptions<APIGetAvailabilityWeekSettings> serviceSettings)
        {
            _httpClientProvider = httpClientProvider;
            _serviceSettings = serviceSettings;
        }


        public async Task<AvailabilityWeek> GetAvailability(DateTime dayOfStartWeek)
        {
            _httpClientProvider.CreateClient(new Uri(_serviceSettings.Value.UrlClient));
            _httpClientProvider.WithBasicAuthenticator(_serviceSettings.Value.UserName, _serviceSettings.Value.Password);
            _httpClientProvider.WithRequest(string.Format(_serviceSettings.Value.RequestTemplate, dayOfStartWeek.ToString("yyyyMMdd")));

            return await _httpClientProvider.GetAsync<AvailabilityWeek>();
        }
    }
}
