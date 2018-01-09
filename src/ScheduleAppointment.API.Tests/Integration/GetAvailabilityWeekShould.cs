using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using ScheduleAppointment.API.Model.DTO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Tests.Integration
{
    [TestFixture(Category = "Integration Tests")]
    public class AvailableWeekSlotsShould
    {


        private const string AVAILABILITY_WEEK_SLOTS_METHOD = "/api/AvailableWeekSlots";

        private readonly TestServer _server;

        private readonly HttpClient _client;



        public AvailableWeekSlotsShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            _client = _server.CreateClient();
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        [Test]
        public async Task Return_bad_request_given_null_or_empty_start_date_of_week()
        {
            // Act
            var response = await _client.GetAsync(AVAILABILITY_WEEK_SLOTS_METHOD);

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }


        [TestCase("20180417")]
        [TestCase("20181118")]
        public async Task Return_bad_request_given_start_date_of_week_is_not_monday(string startDate)
        {
            // Act
            var response = await _client.GetAsync(string.Format("{0}/{1}", AVAILABILITY_WEEK_SLOTS_METHOD, startDate));

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }


        [TestCase("20180312")]
        [TestCase("20181015")]
        public async Task Return_number_of_days_list_of_week_given_valid_start_date_week(string startDate)
        {
            // Act
            var response = await _client.GetAsync(string.Format("{0}/{1}", AVAILABILITY_WEEK_SLOTS_METHOD, startDate));
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeekSlots>(json);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<WeekSlots>(result);
            Assert.AreEqual(result.ConsecutiveDaysOfWeek.Count, 7);
        }
    }
}
