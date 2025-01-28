using AventStack.ExtentReports.Reporter.Config;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace E_Commerce_Project.Utilities.Extent_Reports_Helpers
{
    // The ExtentManager class is responsible for setting up and managing ExtentReports in the project
    public class ExtentManager
    {
        // Lazy initialization of ExtentReports to ensure the instance is created only once when accessed
        private static readonly Lazy<ExtentReports> _lazy = new(() => new ExtentReports());

        // Public static property to access the single instance of ExtentReports
        public static ExtentReports Instance
        {
            get
            {
                return _lazy.Value;  // Return the lazy-loaded ExtentReports instance
            }
        }

        // Static constructor that runs once when the class is accessed
        static ExtentManager()
        {
            // Get the current working directory (where the executable is located)
            string workingDirectory = Environment.CurrentDirectory;

            // Check if the working directory is not null
            if (workingDirectory != null)
            {
                // Get the project's directory by moving two levels up from the working directory
                string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName;

                // Check if the project directory is not null
                if (projectDirectory != null)
                {
                    // Define the path for the generated HTML report (index.html in the project root)
                    string reportPath = projectDirectory + "//index.html";

                    // Create a new ExtentSparkReporter to generate an HTML report at the specified path
                    var htmlReporter = new ExtentSparkReporter(reportPath);

                    // Configure the properties of the report
                    htmlReporter.Config.DocumentTitle = "Extent/NUnit Samples";  // Set the document title
                    htmlReporter.Config.ReportName = "Extent/NUnit Samples";    // Set the report name
                    htmlReporter.Config.Theme = Theme.Dark;  // Set the theme of the report to dark

                    // Attach the configured reporter to the ExtentReports instance
                    Instance.AttachReporter(htmlReporter);

                    // Add system information to the report (useful for reporting context)
                    Instance.AddSystemInfo("Application", "E Commerce Project");
                    Instance.AddSystemInfo("Environment", "QA");
                }
                else
                {
                    // Throw an exception if the project directory is null
                    throw new InvalidOperationException("Project directory could not be determined. Please check the directory structure.");
                }
            }
            else
            {
                // Throw an exception if the working directory is null
                throw new InvalidOperationException("Working directory could not be determined. Please check the application's environment.");
            }
        }

        // Private constructor to prevent instantiation of the ExtentManager class
        // Ensures that the class can only be accessed through the static Instance property
        private ExtentManager() { }
    }
}
