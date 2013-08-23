using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace TryToLogSpecFlowActivity
{
    using System.Diagnostics;
    using IOPath = System.IO.Path;

    [Binding]
    public class Feature1Steps
    {
        [Given(@"Step One")]
        public void GivenStepOne()
        {
        }
        
        [When(@"Step Two")]
        public void WhenStepTwo()
        {
        }
        
        [When(@"Step Two, But Fail!")]
        public void WhenStepTwoButFail()
        {
            throw new ApplicationException("Oops...");
        }
        
        [Then(@"Step Three")]
        public void ThenStepThree()
        {
        }

        [BeforeScenario]
        public void OnBeforeScenario()
        {
            var stopWatch = Stopwatch.StartNew();
            ScenarioContext.Current.Set<Stopwatch>(stopWatch, "02e6e734f7834235921443f7f0afad2d");
        }

        [AfterScenario]
        public void OnAfterScenario()
        {
            var stopWatch = ScenarioContext.Current.Get<Stopwatch>("02e6e734f7834235921443f7f0afad2d");
            stopWatch.Stop();
            var featureInfo = FeatureContext.Current.FeatureInfo;
            var scenarioInfo = ScenarioContext.Current.ScenarioInfo;
            var testError = ScenarioContext.Current.TestError;
            var rowText = string.Format("{0:g},{1},{2},{3},{4}",
                DateTime.Now,
                stopWatch.ElapsedMilliseconds,
                testError == null ? "Success" : "Fail",
                featureInfo.Title,
                scenarioInfo.Title
                );
            TestLog.WriteLine(rowText);
        }
    }

    [TestClass]
    public class TestLog
    {
        public static string Path { get; private set; }

        [AssemblyInitialize]
        public static void OnAssemblyInitialize(TestContext context)
        {
            Path = IOPath.GetFullPath(IOPath.Combine(context.TestDir, string.Format("..\\TestLog_{0:yyyy-MM-dd HH_mm_ss}.csv", DateTime.Now)));
            File.WriteAllLines(Path, new[] { 
                "date-time,duration,result,feature-title,scenario-title"
            });
        }

        public static void WriteLine(string text)
        {
            File.AppendAllLines(Path, new[] { text });
        }
    }
}
