using OpenQA.Selenium;

namespace TestRail.Steps
{
    public class BaseStep
    {
        protected IWebDriver _driver;

        public BaseStep(IWebDriver driver)
        {
            _driver = driver;
        }
    }
}
