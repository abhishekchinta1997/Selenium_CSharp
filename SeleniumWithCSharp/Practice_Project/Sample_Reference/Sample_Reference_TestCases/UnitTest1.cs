using E_Commerce_Project.Sample_Reference.Sample_Reference_PageObjects;
using Practice_Project.Utilities.Base;

namespace Practice_Project.Sample_Reference.Sample_Reference_TestCases
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    public class UnitTest1 : BaseClass
    {
        [Test]
        public void Test1()
        {
            SampleLoginPage lp = new(GetDriver());
            lp.ValidLogin("rahulshettyacademy", "learning");
        }

        [Test]
        public void Test2()
        {
            SampleLoginPage lp = new(GetDriver());
            lp.ValidLogin("rahulshettyacademy", "learning");
        }

    }
}
