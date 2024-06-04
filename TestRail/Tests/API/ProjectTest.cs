using NLog;
using RestSharp;
using System.Net;
using TestRail.Services_API;

namespace TestRail.Tests.API
{
    public class ProjectTest : BaseApiTest
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        [Test]
        public void GetCorrectProject()
        {
            var endPoint = "index.php?/projects/overview/1";
            var services = new ApiServices();

            var restOption = services.CreateOptions(Environment.GetEnvironmentVariable("TESTRAIL_EMAIL"),
                Environment.GetEnvironmentVariable("TESTRAIL_PASSWORD"));
            var response = services.CreateGetRequest(endPoint, new RestClient(restOption));

            _logger.Info(response.Content);
            Assert.That(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
