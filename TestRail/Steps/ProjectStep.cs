using OpenQA.Selenium;
using TestRail.Models;
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

        public void AddProject(ProjectModel project)
        {
            _addProjectPage.SendProjectName(project.Name);
            _addProjectPage.AddProjectButtonClick();
        }
    }
}
