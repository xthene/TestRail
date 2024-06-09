using OpenQA.Selenium;
using TestRail.Element;

namespace TestRail.Pages
{
    public class MilestonesPage : BasePage
    {
        private readonly By _messageSuccess = By.XPath("//div[@data-testid='messageSuccessDivBox']");
        private readonly By _deleteButton = By.XPath("//a[contains(text(), 'test')]//ancestor::div//following-sibling::div//child::a[contains(@class, 'deleteLink')]");
        private readonly By _deleteDialog = By.XPath("//div[@id='deleteDialog']");
        private readonly By _deleteDialogMessage = By.XPath("//div[@id='deleteDialog']//descendant::p[contains(@class, 'dialog-message')]");
        private readonly By _deleteDialogOkButton = By.XPath("//a[@data-testid='caseFieldsTabDeleteDialogButtonOk']");
        private readonly By _milestoneTitles = By.XPath("//div[contains(@class, 'summary-title')]//a");

        private readonly string endPoint = "/index.php?/milestones/overview/{project_id}";

        protected IWebDriver Driver { get; set; }

        public MilestonesPage(IWebDriver driver, bool openPageByUrl = false) : base(driver, openPageByUrl)
        {
            Driver = driver;
        }

        public UIElement MessageSuccess() => new(Driver, _messageSuccess);
        public Button DeleteButton() => new(Driver, _deleteButton);
        public UIElement DeleteDialog() => new(Driver, _deleteDialog);
        public UIElement DeleteDialogMessage() => new(Driver, _deleteDialogMessage);
        public Button DeleteDialogOkButton() => new(Driver, _deleteDialogOkButton);
        public List<UIElement> MilestoneTitles()
        {
            var result = new List<UIElement>();
            foreach (var element in Driver.FindElements(_milestoneTitles))
            {
                result.Add(new(Driver, element));
            }

            return result;
        }

        public string MessageSuccessText() => MessageSuccess().Text;
        public void DeleteButtonClick() => DeleteButton().Click();
        public string DeleteDialogMessageText() => DeleteDialogMessage().Text;
        public void DeleteDialogOkButtonClick() => DeleteDialogOkButton().Click();
        public List<string> MilestoneTitlesText()
        {
            var res = new List<string>();
            foreach(var title in MilestoneTitles())
            {
                res.Add(title.Text);
            }

            return res;
        }
        

        public override string GetEndpoint()
        {
            return endPoint;
        }

        protected override bool EvaluateLoadedStatus()
        {
            throw new NotImplementedException();
        }
    }
}
