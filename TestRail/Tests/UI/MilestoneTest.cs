using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using NLog;
using System.Reflection;
using System.Text.Json;
using TestRail.Models;
using TestRail.Services_API;
using TestRail.Utils;

namespace TestRail.Tests.UI
{
    [TestFixture]
    [Category("UI")]
    public class MilestoneTest : BaseTest
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private string _projectId;
        
        [SetUp]
        public void SetUp()
        {
            using FileStream fs = new FileStream(@"Resources/project.json", FileMode.Open);
            var expectedProject = JsonSerializer.Deserialize<ProjectModel>(fs);

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
        public void AddMilestoneTest()
        {
            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + $"index.php?/milestones/add/{_projectId}");

            using FileStream fs = new FileStream(@"Resources/milestone.json", FileMode.Open);
            var milestone = JsonSerializer.Deserialize<MilestoneModel>(fs);

            Assert.That(MilestoneStep.SuccessfullAddMilestone(milestone).MessageSuccessText,
                Is.EqualTo("Successfully added the new milestone."));
        }

        [Test]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("UI tests")]
        [AllureDescription("try to add milestone with invalid date")]
        public void AddMilestoneWithInvalidDate()
        {
            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + $"index.php?/milestones/add/{_projectId}");

            using FileStream fs = new FileStream(@"Resources/milestone.json", FileMode.Open);
            var milestone = JsonSerializer.Deserialize<MilestoneModel>(fs);

            var page = MilestoneStep.AddMilestoneWithInvalidDate(milestone);

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

            using FileStream fs = new FileStream(@"Resources/milestone.json", FileMode.Open);
            var milestone = JsonSerializer.Deserialize<MilestoneModel>(fs);

            MilestoneStep.SuccessfullAddMilestone(milestone);

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

            using FileStream fs = new FileStream(@"Resources/milestone.json", FileMode.Open);
            var milestone = JsonSerializer.Deserialize<MilestoneModel>(fs);

            MilestoneStep.SuccessfullAddMilestone(milestone);

            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + $"index.php?/milestones/overview/{_projectId}");

            MilestonesPage.DeleteButtonClick();
            MilestonesPage.DeleteDialogOkButtonClick();

            Assert.That(MilestonesPage.MessageSuccessText(), Is.EqualTo("Successfully deleted the milestone (s)."));
        }

        [Test]
        [Category("Failed")]
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
            using FileStream fs = new FileStream(@"Resources/invalidMilestone.json", FileMode.Open);
            var milestone = JsonSerializer.Deserialize<MilestoneModel>(fs);

            var expectedName = milestone.Name.Substring(0, 250);

            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url + $"index.php?/milestones/add/{_projectId}");

            var page = MilestoneStep.SuccessfullAddMilestone(milestone);

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
