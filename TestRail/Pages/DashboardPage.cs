using OpenQA.Selenium;
using TestRail.Element;

namespace TestRail.Pages
{
    public class DashboardPage : BasePage
    {
        private readonly By addProjectButton = By.XPath("//a[@id='sidebar-projects-add']");

        private readonly string _endPoint = "";

        protected IWebDriver Driver { get; set; }
        public override string GetEndpoint()
        {
            return _endPoint;
        }

        public DashboardPage(IWebDriver driver, bool openPageByUrl = false) : base(driver, openPageByUrl)
        {
            Driver = driver;
        }

        public Button AddProjectButton() => new(Driver, addProjectButton);
        public void AddProjectButtonClick() => AddProjectButton().Click();

        protected override bool EvaluateLoadedStatus()
        {
            return AddProjectButton().Enabled;
        }
    }
}
