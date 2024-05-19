using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace TestRail.Utils
{
    public class WaitsHelper
    {
        private IWebDriver driver { get; set; }
        private WebDriverWait _wait;
        private TimeSpan _timeout { get; set; }

        public WaitsHelper(IWebDriver driver)
        {
            this.driver = driver;
            _timeout = TimeSpan.FromSeconds(Configurator.ReadConfiguration().Timeout);
            _wait = new WebDriverWait(driver, _timeout);
        }

        public IWebElement WaitForVisibility(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public bool WaitForElementInvisible(By locator)
        {
            return _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public bool WaitForElementInvisible(IWebElement element)
        {
            try
            {
                _wait.Until(d => !element.Displayed);
                return true;
            }
            catch (NoSuchElementException)
            {
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Element visible after {_timeout} seconds");
            }
        }

        public IWebElement WaitForExist(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementExists(locator));
        }

        public IReadOnlyCollection<IWebElement> WaitForElementsPresence(By locator)
        {
            return _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }
    }
}
