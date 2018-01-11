using Newtonsoft.Json;
using ScheduleAppointment.API.Factories;
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
        public const string TAKE_APPOINTMENT = "availability/TakeSlot";
        public const string USER = "techuser";
        public const string PSW = "secretpassWord";


        private IHttpClientProvider _httpClientProvider;
        private ILoggerProvider _loggerProvider;
        private IFactory<AvailabilityWeek, WeekSlots> _weekSlotsFactory;


        public APIAvailabilityWeekService(
            IHttpClientProvider httpClientProvider,
            ILoggerProvider loggerProvider,
            IFactory<AvailabilityWeek, WeekSlots> weekSlotsFactory)
        {
            _httpClientProvider = httpClientProvider;
            _loggerProvider = loggerProvider;
            _weekSlotsFactory = weekSlotsFactory;
        }


        public async Task TakeAppointment(Appointment takeSlot)
        {
            try
            {
                await _httpClientProvider
                        .CreateClient(URL_CLIENT)
                        .WithBasicAuthenticator(USER, PSW)
                        .PostAsync(TAKE_APPOINTMENT, takeSlot);
            }
            catch (Exception err)
            {
                await _loggerProvider.Log(err);
                throw (err);
            }
        }


        public async Task<WeekSlots> GetAvailabilitySlots(DateTime dayOfStartWeek)
        {
            try
            {
                var result = await GetAvailabilityWeekData(dayOfStartWeek);

                return _weekSlotsFactory.From(result);
            }
            catch (Exception err)
            {
                await _loggerProvider.Log(err);
                throw (err);
            }
        }


        public async Task<AvailabilityWeek> GetAvailabilityWeekData(DateTime dayOfStartWeek)
        {
            try
            {
                var jsonString = await _httpClientProvider
                                            .CreateClient(URL_CLIENT)
                                            .WithBasicAuthenticator(USER, PSW)
                                            .GetStringAsync(string.Format(REQUEST_TEMPLATE, dayOfStartWeek.ToString("yyyyMMdd")));

                if (string.IsNullOrEmpty(jsonString))
                    return new AvailabilityWeek();

                return JsonConvert.DeserializeObject<AvailabilityWeek>(jsonString, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });

            }
            catch (Exception err)
            {
                await _loggerProvider.Log(err);
                throw (err);
            }
        }
    }
}
