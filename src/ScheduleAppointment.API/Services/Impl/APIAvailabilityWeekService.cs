using ScheduleAppointment.API.Model.DTO;
using ScheduleAppointment.API.Providers;
using System;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Services.Impl
{
    public class APIAvailabilityWeekService : IAvailabilityWeekService
    {

        public const string URL_CLIENT = "https://test.draliacloud.net/api/";
        public const string REQUEST_TEMPLATE = "availability/GetWeeklyAvailability/{0}";
        public const string USER = "techuser";
        public const string PSW = "secretpassWord";


        private IHttpClientProvider _httpClientProvider;
        private ILoggerProvider _loggerService;


        public APIAvailabilityWeekService(IHttpClientProvider httpClientProvider, ILoggerProvider loggerService)
        {
            _httpClientProvider = httpClientProvider;
            _loggerService = loggerService;
        }


        public async Task<AvailabilityWeek> GetAvailability(DateTime dayOfStartWeek)
        {
            try
            {
                return
                    await _httpClientProvider
                            .CreateClient(new Uri(URL_CLIENT))
                            .WithBasicAuthenticator(USER, PSW)
                            .GetAsync<AvailabilityWeek>(string.Format(REQUEST_TEMPLATE, dayOfStartWeek.ToString("yyyyMMdd")));
            }
            catch (Exception err)
            {
                await _loggerService.Log(err);
                throw (err);
            }
        }
    }
}
