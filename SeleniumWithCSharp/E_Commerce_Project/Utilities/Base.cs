using AventStack.ExtentReports;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using E_Commerce_Project.Utilities.Extent_Reports_Helpers;

namespace SeleniumCSharpLearning.Utilities
{
    public class Base
    {
        String browserName;


        //report file
        [OneTimeSetUp]
        public void Setup()

        {
            ExtentTestManager.CreateParentTest(GetType().Name);

        }
        
        
        [OneTimeTearDown]
        public void TearDown()
        {
            ExtentManager.GetExtent().Flush();
        }


        // public IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new();


        [SetUp]

        public void StartBrowser()

        {
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
            //Configuration
            browserName = TestContext.Parameters["browserName"];
            if (browserName == null)
            {

                browserName = ConfigurationManager.AppSettings["browser"];
            }
            InitBrowser(browserName);

            getDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            getDriver().Manage().Window.Maximize();
            getDriver().Url = "https://rahulshettyacademy.com/loginpagePractise/";


        }

        public IWebDriver getDriver()

        {
            return driver.Value;
        }

        public void InitBrowser(string browserName)

        {

            switch (browserName)
            {

                case "Firefox":

                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;



                case "Chrome":

                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;


                case "Edge":

                    driver.Value = new EdgeDriver();
                    break;

            }


        }



        [TearDown]
        public void AfterTest()

        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;



            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";

            if (status == TestStatus.Failed)
            {

                ExtentTestManager.GetTest().Fail("Test failed", captureScreenShot(getDriver(), fileName));
                ExtentTestManager.GetTest().Log(Status.Fail, "test failed with logtrace" + stackTrace);

            }
            else if (status == TestStatus.Passed)
            {

            }
            getDriver().Quit();
        }

        public static Media captureScreenShot(IWebDriver driver, String screenShotName)

        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();




        }




    }
}
