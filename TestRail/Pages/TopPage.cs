using OpenQA.Selenium;
using TestRail.Element;

namespace TestRail.Pages
{
    public class TopPage : BasePage
    {
        private readonly By _inProgressLink = By.Id("inProgressLink");
        private readonly By _inProgressMessageTitle = By.XPath("//div[@id='inProgressDropdown']//h2");

        public TopPage(IWebDriver driver, bool openPageByUrl = false) : base(driver, openPageByUrl)
        {
        }

        public UIElement InProgressLink() => new(Driver, _inProgressLink);
        public UIElement InProgressMessageTitle() => new(Driver, _inProgressMessageTitle);

        public void InProgressLinkClick() => InProgressLink().Click();
        public string InProgressMessageTitleText() =>
            InProgressMessageTitle().Text;

        public override string GetEndpoint()
        {
            throw new NotImplementedException();
        }

        protected override bool EvaluateLoadedStatus()
        {
            throw new NotImplementedException();
        }
    }
}
