using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using NLog;
using System.Reflection;
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
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("UI tests")]
        [AllureDescription("positive test for creating milestone")]
        public async Task AddMilestoneTest()
        {
            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + $"index.php?/milestones/add/{_projectId}");

            Assert.That(MilestoneStep.SuccessfullAddMilestone(
                _milestoneService.GetMilestoneById(Configurator.ReadConfiguration().TestMilestoneId).Result).MessageSuccessText,
                Is.EqualTo("Successfully added the new milestone."));
        }

        [Test]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("UI tests")]
        [AllureDescription("try to add milestone with invalid date")]
        [AllureStory("")]
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
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("UI tests")]
        [AllureDescription("dialog window test")]
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
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("UI tests")]
        [AllureDescription("delete existing milestone")]
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
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("UI tests")]
        [AllureDescription("failed test")]
        public void FailedTest()
        {
            Assert.Fail();
        }

        [Test]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("UI tests")]
        [AllureDescription("test for data entry exceeding acceptable limits")]
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

        [Test]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("UI tests")]
        [AllureDescription("Milestone add attachement test")]
        public void AttachementTest()
        {
            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + $"index.php?/milestones/add/{_projectId}");

            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources",
            "test.txt");

            AddMilestonePage.SendAttachement(filePath);

            Assert.That(AddMilestonePage.IsDescriptionInputAttachementListItemContains());
        }
    }
}
