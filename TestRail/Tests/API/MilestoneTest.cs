using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using NLog;
using RestSharp;
using System.Net;
using System.Text.Json;
using TestRail.Models;
using TestRail.Services;
using TestRail.Utils;

namespace TestRail.Tests.API
{
    [TestFixture]
    [Category("API")]
    public class MilestoneTest : BaseApiTest
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private string projectId;
        private string milestoneId;

        [SetUp]
        public void SetUp()
        {
            using FileStream fs = new FileStream(@"Resources/project.json", FileMode.Open);
            var expectedProject = JsonSerializer.Deserialize<ProjectModel>(fs);

            projectId = ApiSteps.CreateProjectAndReturnId(expectedProject);
        }

        [TearDown]
        public void TearDown()
        {
            ApiSteps.DeleteProjectById(projectId);
        }

        [Test]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("API tests")]
        [AllureDescription("create milestone")]
        public void CreateMilestone()
        {
            const string endPoint = "index.php?/api/v2/add_milestone/{project_id}";

            using FileStream fs = new FileStream(@"Resources/milestone.json", FileMode.Open);
            var milestone = JsonSerializer.Deserialize<MilestoneModel>(fs);

            var request = new RestRequest(endPoint).AddJsonBody(milestone);
            request.AddUrlSegment("project_id", projectId);
            var response = Client.ExecutePost<MilestoneModel>(request);

            var actualMilestone = Newtonsoft.Json.JsonConvert.DeserializeObject<MilestoneModel>(response.Content);

            Assert.That(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("API tests")]
        [AllureDescription("get created milestone by project id")]
        public void GetMilestoneByProject()
        {
            const string endPoint = "index.php?/api/v2/get_milestones/{project_id}";

            using FileStream fs = new FileStream(@"Resources/milestone.json", FileMode.Open);
            var milestone = JsonSerializer.Deserialize<MilestoneModel>(fs);
            ApiSteps.CreateMilestoneAndReturnId(milestone, projectId);

            var request = new RestRequest(endPoint).AddUrlSegment("project_id", projectId);
            var response = Client.ExecuteGet<MilestoneModel>(request);

            var result = DataHelper.GetListFromContent(response.Content);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode == HttpStatusCode.OK);
                Assert.That(result.Count == 1);
            });
        }

        [Test]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("API tests")]
        [AllureDescription("get created milestone by id")]
        public void GetMilestoneById()
        {
            const string endPoint = "index.php?/api/v2/get_milestone/{milestone_id}";

            using FileStream fs = new FileStream(@"Resources/milestone.json", FileMode.Open);
            var milestone = JsonSerializer.Deserialize<MilestoneModel>(fs);
            milestoneId = ApiSteps.CreateMilestoneAndReturnId(milestone, projectId);

            var request = new RestRequest(endPoint).AddUrlSegment("milestone_id", milestoneId);
            var response = Client.ExecuteGet<MilestoneModel>(request);

            var actualMilestone = Newtonsoft.Json.JsonConvert.DeserializeObject<MilestoneModel>(response.Content);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode == HttpStatusCode.OK);
                Assert.That(actualMilestone.Name == milestone.Name);
            });
        }

        [Test]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("API tests")]
        [AllureDescription("get deleted milestone by id")]
        public void GetRemovedMilestoneById()
        {
            const string endPoint = "index.php?/api/v2/get_milestone/{milestone_id}";

            using FileStream fs = new FileStream(@"Resources/milestone.json", FileMode.Open);
            var milestone = JsonSerializer.Deserialize<MilestoneModel>(fs);
            milestoneId = ApiSteps.CreateMilestoneAndReturnId(milestone, projectId);

            ApiSteps.DeleteMilestoneById(milestoneId);

            var request = new RestRequest(endPoint).AddUrlSegment("milestone_id", milestoneId);
            var response = Client.ExecuteGet<MilestoneModel>(request);

            var actualMilestone = Newtonsoft.Json.JsonConvert.DeserializeObject<MilestoneModel>(response.Content);
            
            Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("API tests")]
        [AllureDescription("delete emilestone")]
        public void RemoveMilestone()
        {
            const string endPoint = "index.php?/api/v2/delete_milestone/{milestone_id}";

            using FileStream fs = new FileStream(@"Resources/milestone.json", FileMode.Open);
            var milestone = JsonSerializer.Deserialize<MilestoneModel>(fs);
            milestoneId = ApiSteps.CreateMilestoneAndReturnId(milestone, projectId);

            var request = new RestRequest(endPoint).AddUrlSegment("milestone_id", milestoneId); ;
            request.AddHeader("Content-Type", "application/json");
            var response = Client.ExecutePost(request);

            Assert.That(response.IsSuccessStatusCode);
        }
    }
}
