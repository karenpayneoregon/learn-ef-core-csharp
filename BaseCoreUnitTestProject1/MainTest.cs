using System;
using System.Collections;
using System.Diagnostics;
using BaseCoreUnitTestProject1.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseCoreUnitTestProject1
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public void TestMethod1()
        {
            // arrange
            var test = Environment.GetEnvironmentVariables();

            foreach (DictionaryEntry entry in test)
            {
                Console.WriteLine($"{entry.Key} - {entry.Value}");
            }
            Debug.WriteLine($"[{Environment.GetEnvironmentVariable("dofx")}]");

            // act


            // assert
        }
        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public void TestMethod2()
        {
            // TODO
        }
    }
}
