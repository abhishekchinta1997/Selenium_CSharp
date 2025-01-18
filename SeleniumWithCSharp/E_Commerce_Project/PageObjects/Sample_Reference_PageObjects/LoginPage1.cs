using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.PageObjects.Sample_Reference_PageObjects
{
    public class LoginPage1
    {
        // Page Object Model

        private IWebDriver driver;

        public LoginPage1(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Locators for Login Page
        private By usernameField = By.Id("username");
        private By passwordField = By.Name("password");
        private By checkBox = By.XPath("//div[@class='form-group'][5]/label/span/input");
        private By loginButton = By.CssSelector("input[value='Sign In']");

        // Methods to interact with elements
        public IWebElement GetUsernameField()
        {
            return driver.FindElement(usernameField);
        }

        public IWebElement GetPasswordField()
        {
            return driver.FindElement(passwordField);
        }

        public IWebElement GetCheckBox()
        {
            return driver.FindElement(checkBox);
        }

        public IWebElement GetLoginButton()
        {
            return driver.FindElement(loginButton);
        }

        // Actions for Login
        public void Login(string username, string password)
        {
            GetUsernameField().SendKeys(username);
            GetPasswordField().SendKeys(password);
            GetCheckBox().Click();
            GetLoginButton().Click();
        }
    }
}
