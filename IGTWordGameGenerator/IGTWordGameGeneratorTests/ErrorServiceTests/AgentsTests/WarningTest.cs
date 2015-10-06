using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IGT.WordGameGenerator.Services.ErrorService.Agents;

namespace IGTWordGameGeneratorTests.ErrorServiceTests.AgentsTests
{
    [TestClass]
    public class WarningTest
    {
        [TestMethod]
        public void TestWarningConstructor()
        {
            Warning warning = new Warning(5, 1, new List<string>() {"",""});
            Assert.AreEqual(warning.IsError, false);
        }

        [TestMethod]
        public void TestWarningEquals()
        {
            Warning war1 = new Warning(5, 1, new List<string>() {"",""});
            Warning war2 = new Warning(5, 1, new List<string>() {"",""});
            Assert.AreEqual(war1, war2);
        }

        [TestMethod]
        public void TestWarningNotEquals()
        {
            Warning war1 = new Warning(5, 1, new List<string>() {"",""});
            Warning war2 = new Warning(5, 1, new List<string>() {"",""});
            Assert.AreNotEqual(war1, war2);
        }
    }
}
