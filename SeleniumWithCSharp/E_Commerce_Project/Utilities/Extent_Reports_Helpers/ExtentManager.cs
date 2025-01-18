using AventStack.ExtentReports.Reporter.Config;
using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports.Reporter;

namespace SeleniumCSharpLearning.Utilities
{
    public class ExtentManager
    {
        public static ExtentReports extent;

        public static ExtentReports GetExtent()
        {
            extent = new ExtentReports();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            String reportPath = projectDirectory + "//index.html";

            var htmlReporter = new ExtentSparkReporter(reportPath);
            

            htmlReporter.Config.DocumentTitle = "Extent/NUnit Samples";
            htmlReporter.Config.ReportName = "Extent/NUnit Samples";
            htmlReporter.Config.Theme = Theme.Standard;

            extent.AttachReporter(htmlReporter);
            return extent;
        }

        

    }
}
