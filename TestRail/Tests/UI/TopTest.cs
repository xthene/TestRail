using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using TestRail.Models;
using TestRail.Utils;

namespace TestRail.Tests.UI
{
    [TestFixture]
    [Category("UI")]
    public class TopTest : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url);
            UserStep.SuccessfulLogin(new UserModel()
            {
                UserName = Environment.GetEnvironmentVariable("TESTRAIL_EMAIL"),
                Password = Environment.GetEnvironmentVariable("TESTRAIL_PASSWORD")
            });
        }

        [Test]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("UI tests")]
        [AllureDescription("check that after click by in prohress message link shows message")]
        public void CheckInProgressMessageTest()
        {
            TopPage.InProgressLinkClick();

            Assert.That(TopPage.InProgressMessageTitleText, Is.EqualTo("In Progress"));
        }
    }
}
