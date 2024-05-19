using OpenQA.Selenium;
using TestRail.Models;
using TestRail.Pages;

namespace TestRail.Steps
{
    public class UserStep : BaseStep
    {
        private LoginPage _loginPage;

        public UserStep(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            _loginPage = new LoginPage(driver);
        }

        public DashboardPage SuccessfulLogin(UserModel userModel)
        {
            Login(userModel);

            return new DashboardPage(_driver);
        }

        public LoginPage UnsuccesfulLoginWithoutPassword(UserModel userModel)
        {
            Login(userModel);
            return _loginPage;
        }

        public LoginPage UnsuccesfulLoginWithoutUserNameAndPassword()
        {
            Login();
            return _loginPage;
        }

        private void Login(UserModel userModel = null)
        {
            if (userModel.UserName == null)
                _loginPage.UsernameInput().SendKeys("");
            else
                _loginPage.UsernameInput().SendKeys(userModel.UserName);


            if (userModel.Password == null)
                _loginPage.PasswordInput().SendKeys("");
            else
                _loginPage.PasswordInput().SendKeys(userModel.Password);

            _loginPage.LoginButton().Click();
        }
    }
}
