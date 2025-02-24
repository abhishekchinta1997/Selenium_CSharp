using AventStack.ExtentReports;

namespace E_Commerce_Project.Utilities.Extent_Reports_Helpers
{
    public class ExtentTestManager
    {
        // Dictionary to store parent test instances by their names.
        // This helps in mapping parent test names to their corresponding test instances.
        private static readonly Dictionary<string, ExtentTest> _parentTestMap = [];

        // Thread-local variables to store the parent and child test instances.
        // ThreadLocal ensures that each thread has its own copy of the test instances.
        private static readonly ThreadLocal<ExtentTest> _parentTest = new();
        private static readonly ThreadLocal<ExtentTest> _childTest = new();

        // Lock object to ensure thread safety when creating tests.
        private static readonly object _synclock = new();

        // Creates a new parent test with the specified name.
        public static ExtentTest CreateParentTest(string testName)
        {
            lock (_synclock)  // Ensure thread safety when creating a parent test
            {
                _parentTest.Value = ExtentManager.Instance.CreateTest(testName);
                return _parentTest.Value;
            }
        }

        // Creates a child test under an existing parent test.
        // If the parent test doesn't exist in the map, it creates a new one.
        public static ExtentTest CreateTest(string parentName, string testName)
        {
            lock (_synclock)   // Ensure thread safety when creating child tests
            {
                ExtentTest? parentTest = null;

                // Check if the parent test already exists in the map
                if (!_parentTestMap.TryGetValue(parentName, out ExtentTest? value))
                {
                    // If not, create a new parent test and add it to the dictionary
                    parentTest = ExtentManager.Instance.CreateTest(TestContext.CurrentContext.Test.Type?.Name);
                    _parentTestMap.Add(parentName, parentTest);
                }
                else
                {
                    // Retrieve the existing parent test
                    parentTest = value;
                }

                // Set the parent test and create a child test under it
                _parentTest.Value = parentTest;
                _childTest.Value = parentTest.CreateNode(testName);
                return _childTest.Value;
            }
        }


        // Creates a method-level test under the current parent test.
        public static ExtentTest CreateMethod(string testName)
        {
            lock (_synclock)   // Ensure thread safety when creating a method-level test
            {
                // Check if _parentTest.Value is null before using it
                if (_parentTest.Value == null)
                {
                    throw new InvalidOperationException("Parent test has not been created yet.");
                }

                // Create a new node (method-level test) under the current parent test
                _childTest.Value = _parentTest.Value.CreateNode(testName);
                return _childTest.Value;
            }
        }


        // Retrieves the current method-level test (child test).
        public static ExtentTest? GetMethod()
        {
            lock (_synclock) // Ensure thread safety when accessing the child test
            {
                return _childTest.Value;
            }
        }


        // Retrieves the current test (could be either a parent or child test).
        public static ExtentTest? GetTest()
        {
            lock (_synclock)  // Ensure thread safety when accessing the current test
            {
                return _childTest.Value;
            }
        }

        public static void ClearTest()
        {
            _parentTest.Value = null;
            _childTest.Value = null;
        }


    }
}
