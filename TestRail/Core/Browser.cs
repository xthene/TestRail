using OpenQA.Selenium;
using TestRail.Utils;

namespace TestRail.Core
{
    public class Browser
    {
        public IWebDriver Driver { get; set; }

        public Browser()
        {
            Driver = Configurator.ReadConfiguration().BrowserType.ToLower() switch
            {
                "chrome" => new DriverFactory().GetChromeDriver(),
                "firefox" => new DriverFactory().GetFirefoxDriver(),
                _ => throw new NotSupportedException("This browser type didn't find")
            };

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Configurator.ReadConfiguration().Timeout);
        }
    }
}
