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
        public void CheckInProgressMessageTest()
        {
            TopPage.InProgressLinkClick();

            Assert.That(TopPage.InProgressMessageTitleText, Is.EqualTo("In Progress"));
        }
    }
}
