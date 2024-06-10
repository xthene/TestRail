using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using TestRail.Connector;
using TestRail.Models;
using TestRail.Steps;
using TestRail.Utils;

namespace TestRail.Tests.UI
{
    [TestFixture]
    [Category("UI")]
    public class ProjectTest : BaseTest
    {
        DbConnector connector;
        [SetUp]
        public void Setup()
        {
            connector = new DbConnector();
 
            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url);
            UserStep.SuccessfulLogin(new UserModel()
            {
                UserName = Environment.GetEnvironmentVariable("TESTRAIL_EMAIL"),
                Password = Environment.GetEnvironmentVariable("TESTRAIL_PASSWORD")
            }).AddProjectButtonClick();
        }

        [Test]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("UI tests")]
        [AllureDescription("check limit values of project name field with 1 symbol")]
        public void CheckLimitValuesProjectNameField_1Symbol()
        {
            var name = DataHelper.CreateStringByLength(1);
            AddProjectPage.SendProjectName(name);

            var expectedResult = name;
            var actualResult = AddProjectPage.ProjectNameInput().GetAttribute("value");

            Assert.That(expectedResult.Equals(actualResult));
        }

        [Test]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("UI tests")]
        [AllureDescription("check limit values of project name field with 250 symbols")]
        public void CheckLimitValuesProjectNameField_250Symbols()
        {
            var name = DataHelper.CreateStringByLength(250);
            AddProjectPage.SendProjectName(name);

            var expectedResult = name;
            var actualResult = AddProjectPage.ProjectNameInput().GetAttribute("value");

            Assert.That(expectedResult.Equals(actualResult));
        }

        [Test]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("UI tests")]
        [AllureDescription("test project name field with a value exceeding the acceptable value")]
        public void CheckLimitValuesProjectNameField_251Symbols()
        {
            var name = DataHelper.CreateStringByLength(251);
            AddProjectPage.SendProjectName(name);

            var expectedResult = name.Remove(name.Length - 1, 1);
            var actualResult = AddProjectPage.ProjectNameInput().GetAttribute("value");

            Assert.That(expectedResult.Equals(actualResult));
        }
    }
}
