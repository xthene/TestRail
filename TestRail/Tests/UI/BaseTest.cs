using DotNetEnv;
using OpenQA.Selenium;
using System.Reflection;
using TestRail.Core;
using TestRail.Pages;
using TestRail.Steps;

namespace TestRail.Tests.UI
{
    public class BaseTest
    {
        public IWebDriver Driver { get; set; }

        public NavigationStep NavigationStep { get; set; }
        public UserStep UserStep { get; set; }
        public ProjectStep ProjectStep { get; set; }
        public MilestoneStep MilestoneStep { get; set; }
        public AddProjectPage AddProjectPage { get; set; }
        public AddMilestonePage AddMilestonePage { get; set; }

        [OneTimeSetUp]
        public static void SetupEnvVariables()
        {
            var pathToEnvFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                ".env");
            Env.Load(pathToEnvFile);
        }

        [SetUp]
        public void Setup()
        {
            Driver = new Browser().Driver;

            NavigationStep = new NavigationStep(Driver);
            UserStep = new UserStep(Driver);
            ProjectStep = new ProjectStep(Driver);
            MilestoneStep = new MilestoneStep(Driver);

            AddProjectPage = new AddProjectPage(Driver);
            AddMilestonePage = new AddMilestonePage(Driver);
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
