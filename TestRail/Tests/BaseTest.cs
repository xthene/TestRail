using OpenQA.Selenium;
using TestRail.Core;
using TestRail.Pages;
using TestRail.Steps;

namespace TestRail.Tests
{
    public class BaseTest
    {
        public IWebDriver Driver { get; set; }

        public NavigationStep NavigationStep { get; set; }
        public UserStep UserStep { get; set; }
        public ProjectStep ProjectStep { get; set; }
        public AddProjectPage AddProjectPage { get; set; }

        [SetUp]
        public void Setup()
        {
            Driver = new Browser().Driver;

            NavigationStep = new NavigationStep(Driver);
            UserStep = new UserStep(Driver);
            ProjectStep = new ProjectStep(Driver);

            AddProjectPage = new AddProjectPage(Driver);
        }

        [TearDown]
        public void TearDown()
        {
            //if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            //{
            //    var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            //    var screenshotBytes = screenshot.AsByteArray;
            //    AllureApi.AddAttachment("screenshot", "image/png", screenshotBytes);
            //}

            Driver.Dispose();
        }
    }
}
