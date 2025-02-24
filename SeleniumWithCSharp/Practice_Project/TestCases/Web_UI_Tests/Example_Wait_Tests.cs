using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Practice_Project.Utilities.Base;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Project.TestCases.Web_UI_Tests
{
    public class Example_Wait_Tests : BaseClass
    {
        [Test, Order(1)]
        public void Test_Implicit_Wait()
        {
            IWebDriver driver = GetDriver();   // Get the WebDriver instance from BaseClass
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);  // Add implicit wait for 10 seconds

            driver.Url = "https://the-internet.herokuapp.com/drag_and_drop";  // Navigate to the Drag and Drop page

            // Locate the two draggable elements (Box A and Box B)
            IWebElement boxA = driver.FindElement(By.Id("column-a"));
            IWebElement boxB = driver.FindElement(By.Id("column-b"));

            // Assert that both boxes are displayed before performing any actions
            Assert.That(boxA.Displayed, Is.True, "Box A is not displayed.");
            LogInfo("Box A is Displayed");
            Assert.That(boxB.Displayed, Is.True, "Box B is not displayed.");
            LogInfo("Box A is Displayed");

            // Create an Actions object to perform drag and drop
            Actions actions = new(driver);

            // Perform the drag and drop action: Drag Box A to Box B's position
            actions.DragAndDrop(boxA, boxB).Build().Perform();

            // Assert that the boxes have been swapped (Box A should now be in Box B's position)
            Assert.That(boxA.Text, Is.EqualTo("B"), "Box A did not move to Box B's position.");
            LogInfo("Box A is now moved to Box B's position.");
            Assert.That(boxB.Text, Is.EqualTo("A"), "Box B did not move to Box A's position.");
            LogInfo("Box B is now moved to Box A's position.");
        }

        [Test, Order(2)]
        public void Test_Explicit_Wait()
        {
            IWebDriver driver = GetDriver();   // Get the WebDriver instance from BaseClass
            driver.Url = "http://the-internet.herokuapp.com/dynamic_controls";  // Navigate to the Drag and Drop page
            LogInfo("Navigated to 'http://the-internet.herokuapp.com/dynamic_controls'");

            // Create a WebDriverWait instance
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));

            // Click the "Remove" button to dynamically remove the checkbox
            IWebElement removeButton = driver.FindElement(By.XPath("//button[normalize-space()='Remove']"));
            removeButton.Click();
            LogInfo("'Remove' button clicked");

            // Use WebDriverWait to wait for the checkbox to become invisible
            bool isCheckboxInvisible = wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//input[@type='checkbox']")));

            // Assert that the checkbox is invisible (i.e., it has been removed)
            Assert.That(isCheckboxInvisible, Is.True, "Checkbox is still visible, expected it to be removed.");
            LogInfo("Checkbox is invisible, as expected.");

            IWebElement msg = driver.FindElement(By.XPath("//p[@id='message']"));
            Assert.That(msg.Displayed, Is.True, "Message is not displayed.");
            CaptureScreenShot(driver, "Msg");
            LogInfo("Message is Displayed, as expected");
        }


        [Test, Order(3)]
        public void Test_Fluent_Wait()
        {
            IWebDriver driver = GetDriver();   // Get the WebDriver instance from BaseClass
            driver.Url = "http://the-internet.herokuapp.com/dynamic_controls";  // Navigate to the Drag and Drop page
            LogInfo("Navigated to 'http://the-internet.herokuapp.com/dynamic_controls'");

            // Create a Fluent Wait (WebDriverWait) instance with custom polling and ignoring exceptions
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(20));
            wait.PollingInterval = TimeSpan.FromSeconds(2);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            // Click the "Remove" button to dynamically remove the checkbox
            IWebElement removeButton = driver.FindElement(By.XPath("//button[normalize-space()='Remove']"));
            removeButton.Click();
            LogInfo("'Remove' button clicked");

            // Fluent wait: Check if the checkbox is not displayed (i.e., it has been removed)
            bool isCheckboxRemoved = wait.Until(driver1 =>
            {
                var checkboxes = driver1.FindElements(By.XPath("//input[@type='checkbox']"));
                return !checkboxes.Any();  // Return true when checkbox is removed (list is empty)
            });

            // Assert that the checkbox is invisible (i.e., it has been removed)
            Assert.That(isCheckboxRemoved, Is.True, "Checkbox is still visible, expected it to be removed.");
            LogInfo("Checkbox is invisible, as expected.");

            IWebElement msg = driver.FindElement(By.XPath("//p[@id='message']"));
            Assert.That(msg.Displayed, Is.True, "Message is not displayed.");
            CaptureScreenShot(driver, "Msg");
            LogInfo("Message is Displayed, as expected");
        }


    }
}
