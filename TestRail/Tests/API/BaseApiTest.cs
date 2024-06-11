using Allure.NUnit;
using Allure.NUnit.Attributes;
using RestSharp;
using TestRail.Services_API;

namespace TestRail.Tests.API
{
    [AllureNUnit]
    public class BaseApiTest
    {
        protected ApiServices ApiServices { get; private set; }
        protected ApiSteps ApiSteps { get; private set; }
        protected RestClient Client { get; private set; }
        private RestClientOptions _restOption;

        [SetUp]
        [AllureBefore("RestClient set up")]
        public void SetupApi()
        {
            ApiServices = new ApiServices();
            ApiSteps = new ApiSteps();

            _restOption = ApiServices.CreateOptions(Environment.GetEnvironmentVariable("TESTRAIL_EMAIL"),
                Environment.GetEnvironmentVariable("TESTRAIL_PASSWORD"));
            Client = ApiServices.SetUpClient(_restOption);
        }

        [TearDown]
        [AllureAfter("RestClient dispose")]
        public void TearDownApi()
        {
            Client.Dispose();
        }
    }
}
