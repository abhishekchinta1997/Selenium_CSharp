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

namespace Practice_Project.Utilities.Base
{
    public class BaseClass
    {

        public ThreadLocal<IWebDriver> driver = new();
        public IWebDriver GetDriver()
        {
            if (driver.Value == null)
            {
                // Handle the null case, perhaps throw an exception or initialize the driver
                throw new InvalidOperationException("Driver is not initialized.");
            }
            return driver.Value;
        }


        [OneTimeSetUp]
        public void Setup()
        {
            //ExtentTestManager.CreateParentTest(GetType().Name);
        }


        [SetUp]
        public void StartBrowser()
        {
            string parentName = GetType().FullName ?? "Unknown";
            ExtentTestManager.CreateTest(parentName, TestContext.CurrentContext.Test.Name);

            //Configuration
            string browserName = TestContext.Parameters["browserName"] ?? ConfigurationManager.AppSettings["browser"]!;

            if (string.IsNullOrEmpty(browserName))
            {
                throw new ArgumentException("Browser name cannot be null or empty", nameof(browserName));
            }

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
                default:
                    throw new ArgumentException(nameof(browserName));
            }

            // GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            GetDriver().Manage().Window.Maximize();
        }

        
        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                             ? ""
                             : string.Format("<pre>{0}</pre>",
                                        "Message: " + "<br>" +
                                        TestContext.CurrentContext.Result.Message +
                                        "<br><br>" +
                                        "Stack Trace: " + "<br>" +
                                        TestContext.CurrentContext.Result.StackTrace);

            DateTime time = DateTime.Now;
            string screenShot_name = "Screenshot_" + time.ToString("h_mm_ss") + ".png";

            if (status == TestStatus.Failed)
            {
                // Apply red color formatting for the message and stack trace
                string failureMessage = "<font color='red'>" + "Test failed" + "</font>";
                string failureStackTrace = string.IsNullOrEmpty(stackTrace) ? "" : "<font color='red'>" + stackTrace + "</font>";

                ExtentTestManager.GetTest()?.Fail(failureMessage, CaptureScreenShot(GetDriver(), screenShot_name));
                if (!string.IsNullOrEmpty(stackTrace))
                {
                    ExtentTestManager.GetTest()?.Fail(failureStackTrace);
                    ExtentTestManager.GetTest()?.Fail("Test Ended with Fail");
                }
            }
            else if (status == TestStatus.Skipped || status == TestStatus.Warning || status == TestStatus.Inconclusive)
            {
                ExtentTestManager.GetTest()?.Warning("Test skipped");
                if (!string.IsNullOrEmpty(stackTrace))
                {
                    ExtentTestManager.GetTest()?.Warning(stackTrace);
                    ExtentTestManager.GetTest()?.Warning("Test Ended with Warning");
                }
            }
            else if (status == TestStatus.Passed)
            {
                ExtentTestManager.GetTest()?.Pass("Test Ended with Pass");
            }

            GetDriver().Quit();
        }


        [OneTimeTearDown]
        public void TearDown()
        {
            ExtentManager.Instance.Flush();
        }


        public static Media CaptureScreenShot(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }


        public static ExtentTest? LogPass(string msg)
        {
            return ExtentTestManager.GetTest()?.Pass(msg);
            //return ExtentTestManager.GetTest();
        }

        public static ExtentTest? LogFail(string msg)
        {
            return ExtentTestManager.GetTest()?.Fail(msg);
            //return ExtentTestManager.GetTest();
        }

        public static ExtentTest? LogInfo(String msg)
        {
            return ExtentTestManager.GetTest()?.Info(msg);
        }

    }
}
