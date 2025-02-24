using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace E_Commerce_Project.Sample_Reference.Sample_Reference_PageObjects
{
    public class SampleLoginPage
    {

        // Page Factory
        private readonly IWebDriver driver;

        public SampleLoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);

            // Ensure elements are initialized
            if (signInButton == null || username == null || password == null || checkBox == null)
            {
                throw new Exception("One or more elements were not initialized correctly.");
            }
        }




        [FindsBy(How = How.Id, Using = "username")]
        private readonly IWebElement username;

        [FindsBy(How = How.Name, Using = "password")]
        private readonly IWebElement password;

        [FindsBy(How = How.XPath, Using = "//div[@class='form-group'][5]/label/span/input")]
        private readonly IWebElement checkBox;

        [FindsBy(How = How.CssSelector, Using = "input[value='Sign In']")]
        private readonly IWebElement signInButton;



        public void ValidLogin(string user, string pass)

        {
            username.SendKeys(user);
            password.SendKeys(pass);
            checkBox.Click();
            signInButton.Click();
        }



    }
}
