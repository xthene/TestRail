using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Drawing;
using TestRail.Utils;

namespace TestRail.Element
{
    public class UIElement
    {
        private readonly Actions _actions;
        private readonly IWebDriver _driver;
        private readonly IWebElement _element;
        private readonly WaitsHelper _waitsHelper;

        private UIElement(IWebDriver driver)
        {
            _driver = driver;
            _actions = new Actions(driver);
            _waitsHelper = new WaitsHelper(driver);
        }

        public UIElement(IWebDriver driver, IWebElement element) : this(driver)
        {
            _element = element;
        }

        public UIElement(IWebDriver driver, By locator) : this(driver)
        {
            _element = _waitsHelper.WaitForExist(locator);
        }

        public IWebElement FindElement(By by)
        {
            return _element.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _element.FindElements(by);
        }

        public void Clear()
        {
            _element.Clear();
        }

        public void SendKeys(string text)
        {
            _element.SendKeys(text);
            if (text != _element.GetAttribute("value"))
                _actions
                    .MoveToElement(_element)
                    .Click()
                    .SendKeys(text)
                    .Build()
                    .Perform();
        }

        public void Submit()
        {
            try
            {
                _element.Submit();
            }
            catch (ElementNotInteractableException)
            {
                _element.SendKeys(Keys.Enter);
            }
        }

        public void Click()
        {
            try
            {
                _element.Click();
            }
            catch (ElementNotInteractableException)
            {
                try
                {
                    _actions
                        .MoveToElement(_element)
                        .Click()
                        .Build()
                        .Perform();
                }
                catch (ElementNotInteractableException)
                {
                    MoveToElement();
                    ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].Click();", _element);
                }
            }
        }

        public string GetAttribute(string attributeName)
        {
            return _element.GetAttribute(attributeName);
        }

        public string GetDomAttribute(string attributeName)
        {
            return _element.GetDomAttribute(attributeName);
        }

        public string GetDomProperty(string propertyName)
        {
            return _element.GetDomProperty(propertyName);
        }

        public string GetCssValue(string propertyName)
        {
            return _element.GetCssValue(propertyName);
        }

        public ISearchContext GetShadowRoot()
        {
            return _element.GetShadowRoot();
        }


        public string TagName => _element.TagName;

        public string Text
        {
            get
            {
                if (_element.Text == null)
                {
                    if (GetAttribute("value") == null)
                        return GetAttribute("innerText");
                    return GetAttribute("value");
                }

                return _element.Text;
            }
        }

        public bool Enabled => _element.Enabled;
        public bool Selected => _element.Selected;
        public Point Location => _element.Location;
        public Size Size => _element.Size;
        public bool Displayed => _element.Displayed;

        public void Hover()
        {
            _actions
                .MoveToElement(_element)
                .Build()
                .Perform();
        }

        public void MoveToElement()
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", _element);
        }
    }
}
