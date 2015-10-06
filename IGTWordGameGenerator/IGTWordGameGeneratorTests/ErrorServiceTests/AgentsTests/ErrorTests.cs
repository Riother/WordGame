using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IGT.WordGameGenerator.Services.ErrorService.Agents;

namespace IGTWordGameGeneratorTests.ErrorServiceTests.AgentsTests
{
    [TestClass]
    public class ErrorTests
    {
        [TestMethod]
        public void TestErrorConstructor()
        {
            Error err = new Error(5, 1, new List<string>() { });
            Assert.AreEqual(err.IsError, true);
        }

        [TestMethod]
        public void TestErrorEquals()
        {
            Error err1 = new Error(5, 1, new List<string>() { });
            Error err2 = new Error(5, 1, new List<string>() { });
            Assert.AreEqual(err1, err2);
        }

        [TestMethod]
        public void TestErrorNotEquals()
        {
            Error err1 = new Error(5, 1, new List<string>() { });
            Error err2 = new Error(5, 2, new List<string>() { });
            Assert.AreNotEqual(err1, err2);
        }
    }
}
