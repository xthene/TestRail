using TestRail.Models;
using TestRail.Steps;
using TestRail.Utils;

namespace TestRail.Tests
{
    public class AddProjectTest : BaseTest
    {
        [SetUp]
        public void Setup()
        {
            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url);
            UserStep.SuccessfulLogin(new UserModel() { UserName = Configurator.ReadConfiguration().Username, Password = Configurator.ReadConfiguration().Password })
                .AddProjectButtonClick();
        }

        [Test]
        public void CheckLimitValuesProjectNameField_1Symbol()
        {
            var name = DataHelper.CreateStringByLength(1);
            AddProjectPage.SendProjectName(name);

            var expectedResult = name;
            var actualResult = AddProjectPage.ProjectNameInput().GetAttribute("value");

            Assert.That(expectedResult.Equals(actualResult));
        }

        [Test]
        public void CheckLimitValuesProjectNameField_250Symbols()
        {
            var name = DataHelper.CreateStringByLength(250);
            AddProjectPage.SendProjectName(name);

            var expectedResult = name;
            var actualResult = AddProjectPage.ProjectNameInput().GetAttribute("value");

            Assert.That(expectedResult.Equals(actualResult));
        }

        [Test]
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
