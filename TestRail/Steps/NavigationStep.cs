using OpenQA.Selenium;
using TestRail.Pages;

namespace TestRail.Steps
{
    public class NavigationStep : BaseStep
    {
        public NavigationStep(IWebDriver driver) : base(driver)
        {
        }

        public AddProjectPage NavigationToAddProjectPage()
        {
            return new AddProjectPage(_driver);
        }
    }
}
