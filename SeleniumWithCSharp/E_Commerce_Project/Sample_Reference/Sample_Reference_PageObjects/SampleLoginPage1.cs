using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.Sample_Reference.Sample_Reference_PageObjects
{
    public class SampleLoginPage1
    {
        // Page Object Model

        private readonly IWebDriver driver;

        public SampleLoginPage1(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Locators for Login Page
        private readonly By usernameField = By.Id("username");
        private readonly By passwordField = By.Name("password");
        private readonly By checkBox = By.XPath("//div[@class='form-group'][5]/label/span/input");
        private readonly By loginButton = By.CssSelector("input[value='Sign In']");

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
