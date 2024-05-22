using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using TestRail.Element;
using TestRail.Models;

namespace TestRail.Pages
{
    public class AddProjectPage : BasePage
    {
        private readonly By nameInput = By.XPath("//input[@id='name']");
        private readonly By announcementArea = By.XPath("//textarea[@id='announcement_display']");
        private readonly By showAnnouncementCheckbox = By.Id("show_announcement");
        private readonly By suiteModeRadioButton = By.XPath("//input[@name='suite_mode']");
        private readonly By testCaseStatusesEnabledCheckbox = By.Id("case_statuses_enabled");
        private readonly By roleSelect = By.XPath("//select[@id='access']");

        private readonly By accessTab = By.XPath("//div[@class='tab-header']/a[contains(text(), 'Access')]");

        private readonly By addProjectButton = By.XPath("//button[@id='accept']");

        private readonly string _endPoint = "";

        protected IWebDriver Driver { get; set; }

        public override string GetEndpoint()
        {
            return _endPoint;
        }

        public AddProjectPage(IWebDriver driver, bool openPageByUrl = false) : base(driver, openPageByUrl)
        {
            Driver = driver;
        }

        public UIElement ProjectNameInput() => new(Driver, nameInput);
        public UIElement AnnouncementInput() => new(Driver, announcementArea);
        public Checkbox ShowAnnouncementCheckbox() => new(Driver, showAnnouncementCheckbox);
        public RadioButton SuiteModeRadioButton() => new(Driver, suiteModeRadioButton);
        public Checkbox TestCaseStatusesEnabledCheckbox() => new(Driver, testCaseStatusesEnabledCheckbox);
        public UIElement AddProjectButton() => new(Driver, addProjectButton);

        public void SendProjectName(string projectName) => 
            ProjectNameInput().SendKeys(projectName);
        public void SendAnnouncement(string announcement) => AnnouncementInput().SendKeys(announcement);

        public void ShowAnnouncementCheckboxCheck(bool value)
        {
            if (value)
                ShowAnnouncementCheckbox().Check();
            else
                ShowAnnouncementCheckbox().Uncheck();
        }

        public void TestCaseStatusesEnabledCheckboxCheck(bool value)
        {
            if (value)
                TestCaseStatusesEnabledCheckbox().Check();
            else
                TestCaseStatusesEnabledCheckbox().Uncheck();
        }

        public void SuiteModeRadioButtonSelectByText(string text) => SuiteModeRadioButton().SelectByText(text);

        public void AccessTabClick() => Driver.FindElement(accessTab).Click();
        public void AddProjectButtonClick() => Driver.FindElement(addProjectButton).Click();

        public SelectElement RoleSelectElement() => new(Driver.FindElement(roleSelect));


        public void AddProjectWithRequiredFields(ProjectModel project)
        {
            SendProjectName(project.Name);
            SendAnnouncement(project.Announcement);
            ShowAnnouncementCheckboxCheck(project.IsShowAnnouncement);
            SuiteModeRadioButtonSelectByText(project.ProjectType);
            TestCaseStatusesEnabledCheckboxCheck(true);
            AccessTabClick();
            RoleSelectElement().SelectByText(project.DefaultAccessRole);
            AddProjectButtonClick();
        }

        protected override bool EvaluateLoadedStatus()
        {
            return AddProjectButton().Enabled;
        }
    }
}
