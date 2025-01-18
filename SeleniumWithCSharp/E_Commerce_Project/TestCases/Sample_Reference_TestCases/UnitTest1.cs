using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumCSharpLearning.Utilities;
using E_Commerce_Project.PageObjects.Sample_Reference_PageObjects;

namespace E_Commerce_Project.TestCases.Sample_Reference_TestCases
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    public class UnitTest1 : Base
    {
        [Test]
        public void Test1()
        {
            LoginPage lp = new LoginPage(getDriver());
            lp.validLogin("rahulshettyacademy", "learning");
        }

        [Test]
        public void Test2()
        {
            LoginPage lp = new LoginPage(getDriver());
            lp.validLogin("rahulshettyacademy", "learning");
        }

    }
}
