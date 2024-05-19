using OpenQA.Selenium;
using TestRail.Utils;

namespace TestRail.Element
{
    public class RadioButton
    {
        private readonly List<UIElement> _uiElements;
        private readonly List<string> _values;
        private readonly List<string> _texts;
        private readonly WaitsHelper _waitsHelper;

        public RadioButton(IWebDriver driver, By locator)
        {
            _uiElements = new List<UIElement>();
            _values = new List<string>();
            _texts = new List<string>();
            _waitsHelper = new WaitsHelper(driver);

            foreach (var element in _waitsHelper.WaitForElementsPresence(locator))
            {
                var uiElement = new UIElement(driver, element);
                _uiElements.Add(uiElement);
                _values.Add(element.GetAttribute("value"));
                _texts.Add(element.FindElement(By.XPath("preceding-sibling::strong")).Text.Trim().ToLower());
            }
        }

        /// <summary>
        ///     Indexes start with 0
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SelectByIndex(int index)
        {
            if (index < _uiElements.Count && index >= 0)
                _uiElements[index].Click();
            else
                throw new ArgumentOutOfRangeException($"Couldn't find element with this index: {index}");
        }

        /// <summary>
        /// Click radio button by attribute value
        /// </summary>
        /// <param name="value"></param>
        public void SelectByValue(string value)
        {
            var index = _values.IndexOf(value);
            _uiElements[index].Click();
        }

        /// <summary>
        /// Click radio button by text
        /// </summary>
        /// <param name="text"></param>
        public void SelectByText(string text)
        {
            _uiElements[_texts.IndexOf(text.ToLower().Trim())].Click();
        }
    }
}
