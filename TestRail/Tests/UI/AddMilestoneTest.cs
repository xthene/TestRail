using MongoDB.Bson;
using TestRail.Connector;
using TestRail.Models;
using TestRail.Services;
using TestRail.Utils;

namespace TestRail.Tests.UI
{
    [TestFixture]
    public class AddMilestoneTest : BaseTest
    {
        private MilestoneService _milestoneService;
        
        [SetUp]
        public void SetUp()
        {
            _milestoneService = new MilestoneService();

            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url);
            UserStep.SuccessfulLogin(new UserModel()
            {
                UserName = Environment.GetEnvironmentVariable("TESTRAIL_EMAIL"),
                Password = Environment.GetEnvironmentVariable("TESTRAIL_PASSWORD")
            });

            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + "index.php?/milestones/add/292");
        }

        [Test]
        public void GetMilestoneTest()
        {
            var expectedResult = new MilestoneModel()
            {
                Id = "665c3a2362e78f3be0af1c30",
                Name = "test",
                References = "TRM-42",
                Description = "test milestone",
                //StartDate = new DateTime(2024, 02, 06),
                //EndDate = new DateTime(2024, 02, 06),
                IsCompleted = true
            };

            var actualResult = _milestoneService.GetMilestoneByid("test").Result;

            Assert.That(actualResult.Equals(expectedResult));
        }

        [Test]
        public async Task AddMilestone()
        {
            MilestoneStep.SuccessfullSetInfo(_milestoneService.GetMilestoneByid("test").Result);

            Assert.Pass();
        }
    }
}
