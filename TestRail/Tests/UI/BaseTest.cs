using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using DotNetEnv;
using OpenQA.Selenium;
using System.Reflection;
using TestRail.Core;
using TestRail.Pages;
using TestRail.Services_API;
using TestRail.Steps;

namespace TestRail.Tests.UI
{
    [AllureNUnit]
    [Parallelizable(ParallelScope.Fixtures)]
    public class BaseTest
    {
        public IWebDriver Driver { get; set; }

        public NavigationStep NavigationStep { get; set; }
        public UserStep UserStep { get; set; }
        public ProjectStep ProjectStep { get; set; }
        public ApiSteps ApiSteps { get; set; }
        public MilestoneStep MilestoneStep { get; set; }
        public AddProjectPage AddProjectPage { get; set; }
        public AddMilestonePage AddMilestonePage { get; set; }
        public TopPage TopPage { get; set; }
        public MilestonesPage MilestonesPage { get; set;}

        [OneTimeSetUp]
        [AllureBefore("Set env variables")]
        public static void SetupEnvVariables()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();

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
            ApiSteps = new ApiSteps();

            AddProjectPage = new AddProjectPage(Driver);
            AddMilestonePage = new AddMilestonePage(Driver);
            TopPage = new TopPage(Driver);
            MilestonesPage = new MilestonesPage(Driver);
        }

        [TearDown]
        [AllureAfter("Driver quite")]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                var screenshotBytes = screenshot.AsByteArray;
                AllureApi.AddAttachment("screenshot", "image/png", screenshotBytes);
            }

            Driver.Dispose();
        }
    }
}
