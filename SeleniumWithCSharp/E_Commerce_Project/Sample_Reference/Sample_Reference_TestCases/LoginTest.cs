using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework.Legacy;
using E_Commerce_Project.Sample_Reference.Sample_Reference_PageObjects;

namespace E_Commerce_Project.Sample_Reference.Sample_Reference_TestCases
{
    public class LoginTest
    {
        private IWebDriver driver;
        private SampleLoginPage1 loginPage;

        [SetUp]
        public void Setup()
        {
            // Initialize WebDriver
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");

            // Initialize the Page Object
            loginPage = new SampleLoginPage1(driver);
        }

        [Test]
        public void TestLogin()
        {
            loginPage.Login("rahulshettyacademy", "learning");
            Console.WriteLine(driver.Title);

            // Assert login success (for demonstration purposes)
            ClassicAssert.IsTrue(driver.Title.Contains("LoginPage Practise | Rahul Shetty Academy"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Close();
        }
    }
}
