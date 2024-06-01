using TestRail.Models;
using TestRail.Steps;
using TestRail.Utils;

namespace TestRail.Tests.UI
{
    public class AddProjectTest : BaseTest
    {
        [SetUp]
        public void Setup()
        {
            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url);
            UserStep.SuccessfulLogin(new UserModel()
            {
                UserName = Environment.GetEnvironmentVariable("TESTRAIL_EMAIL"),
                Password = Environment.GetEnvironmentVariable("TESTRAIL_PASSWORD")
            }).AddProjectButtonClick();
        }

        [Test]
        [Category("Positive")]
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
