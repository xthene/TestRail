using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using System.Text.Json;
using NLog;
using RestSharp;
using System.Net;
using TestRail.Models;
using TestRail.Services;

namespace TestRail.Tests.API
{
    [TestFixture]
    [Category("API")]
    public class ProjectTest : BaseApiTest
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private string _createdProjectId = "";

        [Test]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("API tests")]
        [AllureDescription("create project")]
        public void CreateProject()
        {
            const string endPoint = "index.php?/api/v2/add_project";

            using FileStream fs = new FileStream(@"Resources/project.json", FileMode.Open);
            var expectedProject = JsonSerializer.Deserialize<ProjectModel>(fs);

            var request = new RestRequest(endPoint).AddJsonBody(expectedProject);
            var response = Client.ExecutePost<ProjectModel>(request);

            var actualProject = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectModel>(response.Content);
            _createdProjectId = actualProject.Id;

            _logger.Info(actualProject.Id);

            Assert.That(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("API tests")]
        [AllureDescription("delete project")]
        public void DeleteProject()
        {
            const string _endPoint = "index.php?/api/v2/delete_project/{project_id}";

            var request = new RestRequest(_endPoint).AddUrlSegment("project_id", _createdProjectId);
            request.AddHeader("Content-Type", "application/json");

            var response = Client.ExecutePost(request);

            Assert.That(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
