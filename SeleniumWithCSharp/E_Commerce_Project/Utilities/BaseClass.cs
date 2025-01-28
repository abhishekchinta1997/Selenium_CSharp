using AventStack.ExtentReports;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;
using AventStack.ExtentReports.Model;
using NUnit.Framework.Interfaces;
using E_Commerce_Project.Utilities.Extent_Reports_Helpers;

namespace E_Commerce_Project.Utilities
{
    public class BaseClass
    {
        string browserName;

        [OneTimeSetUp]
        public void Setup()
        {
            ExtentTestManager.CreateParentTest(GetType().Name);
        }


        [OneTimeTearDown]
        public void TearDown()
        {
            ExtentManager.Instance.Flush();
        }


        // public IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new();


        [SetUp]
        public void StartBrowser()
        {
            string parentName = GetType().FullName ?? "Unknown";
            ExtentTestManager.CreateTest(parentName, TestContext.CurrentContext.Test.Name);

            //Configuration
            browserName = TestContext.Parameters["browserName"] ?? ConfigurationManager.AppSettings["browser"]!;

            if (string.IsNullOrEmpty(browserName))
            {
                throw new ArgumentException("Browser name cannot be null or empty", nameof(browserName));
            }
            InitBrowser(browserName);
            GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            GetDriver().Manage().Window.Maximize();
            GetDriver().Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        public IWebDriver GetDriver()
        {
            if (driver.Value == null)
            {
                // Handle the null case, perhaps throw an exception or initialize the driver
                throw new InvalidOperationException("Driver is not initialized.");
            }
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
            var stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                             ? ""
                             : string.Format("<pre>{0}<pre>", "Stack Trace: " + "<br>" + TestContext.CurrentContext.Result.StackTrace + "<br>" + "Message: " + "<br>" + TestContext.CurrentContext.Result.Message);

            DateTime time = DateTime.Now;
            string fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";

            Status logstatus;
            if (status == TestStatus.Failed)
            {
                logstatus = Status.Fail;
                ExtentTestManager.GetTest()?.Fail("Test failed", CaptureScreenShot(GetDriver(), fileName));
            }
            else if (status == TestStatus.Skipped || status == TestStatus.Warning || status == TestStatus.Inconclusive)
            {
                logstatus = Status.Warning;
            }
            else
            {
                logstatus = Status.Pass;
            }

            ExtentTestManager.GetTest()?.Log(logstatus, "Test Ended with " + logstatus);
            if (!string.IsNullOrEmpty(stackTrace))
            {
                ExtentTestManager.GetTest()?.Log(Status.Info, stackTrace);
            }

            GetDriver().Quit();
        }

        public static Media CaptureScreenShot(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }




    }
}
