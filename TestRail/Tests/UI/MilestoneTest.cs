using NLog;
using TestRail.Models;
using TestRail.Services;
using TestRail.Services_API;
using TestRail.Utils;

namespace TestRail.Tests.UI
{
    [TestFixture]
    [Category("UI")]
    public class MilestoneTest : BaseTest
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private MilestoneService _milestoneService;
        private ProjectService _projectService;
        private string _projectId;
        
        [SetUp]
        public void SetUp()
        {            
            _milestoneService = new MilestoneService();
            _projectService = new ProjectService();

            var expectedProject = _projectService.GetProjectById(Configurator.ReadConfiguration().TestProjectId).Result;

            _projectId = ApiSteps.CreateProjectAndReturnId(expectedProject);

            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url);
            UserStep.SuccessfulLogin(new UserModel()
            {
                UserName = Environment.GetEnvironmentVariable("TESTRAIL_EMAIL"),
                Password = Environment.GetEnvironmentVariable("TESTRAIL_PASSWORD")
            });
        }

        [TearDown]
        public void TearDown()
        {
            ApiSteps.DeleteProjectById(_projectId);
        }

        [Test]
        [Category("Positive")]
        public void GetMilestoneTest()
        {
            var expectedResult = new MilestoneModel()
            {
                Id = "665c3a2362e78f3be0af1c30",
                Name = "test",
                References = "TRM-42",
                Description = "test milestone",
                StartDate = new DateTime(2024, 09, 06),
                EndDate = new DateTime(2024, 10, 06),
                IsCompleted = false
            };

            var actualResult = _milestoneService.GetMilestoneById(Configurator.ReadConfiguration().TestMilestoneId).Result;

            Assert.That(actualResult.Equals(expectedResult));
        }

        [Test]
        [Category("Positive")]
        public async Task AddMilestoneTest()
        {
            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + $"index.php?/milestones/add/{_projectId}");

            Assert.That(MilestoneStep.SuccessfullAddMilestone(
                _milestoneService.GetMilestoneById(Configurator.ReadConfiguration().TestMilestoneId).Result).MessageSuccessText,
                Is.EqualTo("Successfully added the new milestone."));
        }

        [Test]
        [Category("Negative")]
        public void AddMilestoneWithInvalidDate()
        {
            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + $"index.php?/milestones/add/{_projectId}");

            var page = MilestoneStep.AddMilestoneWithInvalidDate(
                _milestoneService.GetMilestoneById(Configurator.ReadConfiguration().TestMilestoneId).Result);

            var actualText = page.MessageErrorText();

            Assert.That(actualText, 
                Is.EqualTo("Field Start Date is not in a valid date format.\r\nField End Date is not in a valid date format."));
        }

        [Test]
        [Category("Positive")]
        public void WhenDeleteButtonClickedDialogWindowShowsTest()
        {
            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + $"index.php?/milestones/add/{_projectId}");
            MilestoneStep.SuccessfullAddMilestone(_milestoneService.GetMilestoneById(
                Configurator.ReadConfiguration().TestMilestoneId).Result);

            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + $"index.php?/milestones/overview/{_projectId}");
            MilestonesPage.DeleteButtonClick();

            Assert.That(MilestonesPage.DeleteDialogMessageText(), 
                Is.EqualTo("Really delete this milestone? This irrevocably deletes this milestone for all users and cannot be undone."));
        }

        [Test]
        [Category("Positive")]
        public void DeleteMilestoneTest()
        {
            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + $"index.php?/milestones/add/{_projectId}");
            MilestoneStep.SuccessfullAddMilestone(_milestoneService.GetMilestoneById(
                Configurator.ReadConfiguration().TestMilestoneId).Result);

            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + $"index.php?/milestones/overview/{_projectId}");

            MilestonesPage.DeleteButtonClick();
            MilestonesPage.DeleteDialogOkButtonClick();

            Assert.That(MilestonesPage.MessageSuccessText(), Is.EqualTo("Successfully deleted the milestone (s)."));
        }

        [Test]
        [Category("Negative")]
        public void FailedTest()
        {
            Assert.Fail();
        }

        [Test]
        [Category("Negative")]
        public void AddMilestoneWithNameLengthMoreThan250()
        {
            var expectedName = _milestoneService.GetMilestoneById(Configurator.ReadConfiguration().InvalidMilestoneId)
                .Result.Name.Substring(0, 250);

            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + $"index.php?/milestones/add/{_projectId}");

            var page = MilestoneStep.SuccessfullAddMilestone(
                _milestoneService.GetMilestoneById(Configurator.ReadConfiguration().InvalidMilestoneId).Result);

            _logger.Log(LogLevel.Info, expectedName);
            foreach(var text in page.MilestoneTitlesText())
            {
                _logger.Info(text);
            }

            Assert.That(page.MilestoneTitlesText().Contains(expectedName), Is.True);
        }
    }
}
