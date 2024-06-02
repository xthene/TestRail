using OpenQA.Selenium;
using TestRail.Element;

namespace TestRail.Pages
{
    public class AddMilestonePage : BasePage
    {
        private readonly By nameInput = By.XPath("//input[@id='name']");
        private readonly By referencesInput = By.XPath("//input[@id='reference']");
        private readonly By descriptionInput = By.XPath("//div[@id='description_display']");
        private readonly By startDateInput = By.XPath("//input[@id='start_on']");
        private readonly By endDateInput = By.XPath("//input[@id='due_on']");
        private readonly By isCompletedinput = By.XPath("//input[@id='is_completed']");

        private readonly By acceptButton = By.XPath("//button[@id='accept']");

        private string _endPoint = "index.php?/milestones/add/292";

        protected IWebDriver Driver { get; set; }

        public UIElement NameInput() => new UIElement(Driver, nameInput);
        public UIElement ReferencesInput() => new UIElement(Driver, referencesInput);
        public UIElement DescriptionInput() => new UIElement(Driver, descriptionInput);
        public UIElement StartDateInput() => new UIElement(Driver, startDateInput);
        public UIElement EndDateInput() => new UIElement(Driver, endDateInput);
        public Checkbox IsCompleted() => new Checkbox(Driver, isCompletedinput);
        public Button AcceptButton() => new Button(Driver, acceptButton);

        public void SetName(string name) => NameInput().SendKeys(name);
        public void SetReferences(string references) => ReferencesInput().SendKeys(references);
        public void SetDescription(string description) => DescriptionInput().SendKeys(description);
        public void SetStartDate(DateTime startDate) => StartDateInput().SendKeys(startDate.ToString());
        public void SetEndDate(DateTime endDate) => EndDateInput().SendKeys(endDate.ToString());
        public void SetIsCompleted(bool value)
        {
            if (value)
                IsCompleted().Check();
            else
                IsCompleted().Uncheck();
        }
        public void AcceptButtonClick() => AcceptButton().Click();

        public AddMilestonePage(IWebDriver driver, bool openPageByUrl = true) : base(driver, openPageByUrl)
        {
            Driver = driver;
        }

        public override string GetEndpoint()
        {
            return _endPoint;
        }

        protected override bool EvaluateLoadedStatus()
        {
            throw new NotImplementedException();
        }
    }
}
