using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Legacy;
using E_Commerce_Project.PageObjects.Sample_Reference_PageObjects;

namespace E_Commerce_Project.TestCases.Sample_Reference_TestCases
{
    public class LoginTest
    {
        private IWebDriver driver;
        private LoginPage1 loginPage;

        [SetUp]
        public void Setup()
        {
            // Initialize WebDriver
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");

            // Initialize the Page Object
            loginPage = new LoginPage1(driver);
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
