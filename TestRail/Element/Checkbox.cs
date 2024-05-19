using OpenQA.Selenium;

namespace TestRail.Element
{
    public class Checkbox
    {
        private readonly UIElement _uiElement;

        public Checkbox(IWebDriver driver, By locator)
        {
            _uiElement = new UIElement(driver, locator);
        }

        public bool IsChecked() => _uiElement.Selected;

        public void Check()
        {
            if (!IsChecked())
                _uiElement.Click();
        }

        public void Uncheck()
        {
            if (IsChecked())
                _uiElement.Click();
        }
    }
}
