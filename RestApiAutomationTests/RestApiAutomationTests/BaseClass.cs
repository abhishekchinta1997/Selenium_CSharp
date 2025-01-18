using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;

namespace RestApiAutomationTests
{
    public class BaseClass
    {
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

        public RestClient _client;


        [SetUp]
        public void StartTest()
        {
            // Initialize RestClient with base URL
            _client = new RestClient("https://jsonplaceholder.typicode.com");
        }


        [TearDown]
        public void AfterTest()
        {
            _client.Dispose();
        }

        public static ExtentTest TestLog(string message)
        {
            return ExtentTestManager.CreateTest(message);
        }

    }
}
