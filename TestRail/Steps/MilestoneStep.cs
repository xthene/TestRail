using OpenQA.Selenium;
using TestRail.Models;
using TestRail.Pages;

namespace TestRail.Steps
{
    public class MilestoneStep : BaseStep
    {
        public AddMilestonePage AddMilestonePage { get; set; }

        public MilestoneStep(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            AddMilestonePage = new AddMilestonePage(driver);
        }

        public void SuccessfullSetInfo(MilestoneModel milestone)
        {
            SetInfo(milestone);
        }


        private void SetInfo(MilestoneModel milestone)
        {
            if (milestone.Name == null)
                AddMilestonePage.NameInput().SendKeys("");
            else
                AddMilestonePage.NameInput().SendKeys($"{milestone.Name}");

            if (milestone.References == null)
                AddMilestonePage.ReferencesInput().SendKeys("");
            else
                AddMilestonePage.ReferencesInput().SendKeys($"{milestone.References}");

            if (milestone.Description == null)
                AddMilestonePage.DescriptionInput().SendKeys("");
            else
                AddMilestonePage.DescriptionInput().SendKeys($"{milestone.Description}");

            //if (milestone.StartDate == null)
            //    AddMilestonePage.StartDateInput().SendKeys("");
            //else
            //    AddMilestonePage.StartDateInput().SendKeys($"{milestone.StartDate}");

            //if (milestone.EndDate == null)
            //    AddMilestonePage.EndDateInput().SendKeys("");
            //else
            //    AddMilestonePage.EndDateInput().SendKeys($"{milestone.EndDate}");

            AddMilestonePage.SetIsCompleted(milestone.IsCompleted);

            AddMilestonePage.AcceptButtonClick();

        }
    }
}
