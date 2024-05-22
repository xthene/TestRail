using OpenQA.Selenium;
using TestRail.Pages;

namespace TestRail.Steps
{
    public class ProjectStep : BaseStep
    {
        private AddProjectPage _addProjectPage;

        public ProjectStep(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            _addProjectPage = new AddProjectPage(driver);
        }
    }
}
