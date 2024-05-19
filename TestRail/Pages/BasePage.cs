using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestRail.Utils;

namespace TestRail.Pages
{
    public abstract class BasePage : LoadableComponent<BasePage>
    {
        protected IWebDriver Driver { get; set; }

        public BasePage(IWebDriver driver, bool openPageByUrl = false)
        {
            Driver = driver;
            if (openPageByUrl)
                ExecuteLoad();
            //Load();
        }

        public abstract string GetEndpoint();

        protected override void ExecuteLoad()
        {
            Driver.Navigate().GoToUrl(Configurator.ReadConfiguration().Url.Trim() + GetEndpoint());
        }
    }
}
