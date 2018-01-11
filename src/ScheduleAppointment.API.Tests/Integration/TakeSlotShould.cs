using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Tests.Integration
{
    [TestFixture(Category = "Integration Tests")]
    public class TakeSlotShould
    {


        private const string TAKE_METHOD = "/api/availability/takeslot";

        private const string VALID_SLOT = "{ 'Start':'2017-06-13 11:00:00', 'End':'2017-06-13 12:00:00', 'Patient' : { 'Name' : 'Mario', 'SecondName' : 'Neta', 'Email' : 'mario@myspace.es', 'Phone' : '555 44 33 22' }, 'Comments':'my arm hurts a lot' } ";

        private const string SLOT_WITH_EMPTY_OR_NULL_MANDATORY_FIELDS = "{ 'Start':'', 'End':'2017-06-13 12:00:00', 'Patient' : { 'Name' : '', 'SecondName' : 'Neta', 'Email' : 'mario@myspace.es' }, 'Comments':'my arm hurts a lot' } ";

        private readonly TestServer _server;

        private readonly HttpClient _client;


        public TakeSlotShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            _client = _server.CreateClient();
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        [Test]
        public async Task Return_bad_request_given_null_parameter()
        {
            // Act
            var response = await _client.PostAsync(TAKE_METHOD, GetHttpContentFromJSON(string.Empty));

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }


        [Test]
        public async Task Return_bad_request_given_parameter_with_empty_or_null_mandatory_fields()
        {
            // Act
            var response = await _client.PostAsync(TAKE_METHOD, GetHttpContentFromJSON(SLOT_WITH_EMPTY_OR_NULL_MANDATORY_FIELDS));

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }


        [Test]
        public async Task Return_ok_given_valid_parameter_slot()
        {
            // Act
            var response = await _client.PostAsync(TAKE_METHOD, GetHttpContentFromJSON(VALID_SLOT));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }


        private ByteArrayContent GetHttpContentFromJSON(string data)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }
    }
}
