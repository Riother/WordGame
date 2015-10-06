using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IGT.WordGameGenerator.Services.ErrorService;

namespace IGTWordGameGeneratorTests.ErrorServiceTests
{
    [TestClass]
    public class ErrorServiceTest
    {
        private void ClearState()
        {
            ErrorService.Instance.Clear();
        }

        [TestMethod]
        public void TestErrorServiceConstruction()
        {
            Assert.AreEqual(ErrorService.Instance.HasErrors(), false);
            Assert.AreEqual(ErrorService.Instance.HasWarnings(), false);
            ClearState();
        }

        [TestMethod]
        public void TestReportError()
        {
            int? testId = null;
            testId = ErrorService.Instance.ReportError(0, new List<string>() { "Valid Words" }, testId);
            Assert.AreEqual(testId, 0);
            Assert.AreEqual(ErrorService.Instance.HasErrors(), true);
            ClearState();
        }

        [TestMethod]
        public void TestResolveError()
        {
            int? testId = null;
            testId = ErrorService.Instance.ReportError(0, new List<string>() { "Valid Words" }, testId);
            ErrorService.Instance.ResolveError(0, testId);
            Assert.AreEqual(ErrorService.Instance.HasErrors(), false);
            ClearState();
        }

        [TestMethod]
        public void TestReportWarning()
        {
            int? testId = null;
            testId = ErrorService.Instance.ReportWarning(0, new List<string>() { "Valid Words", "Banned Words" }, testId);
            Assert.AreEqual(testId, 0);
            Assert.AreEqual(ErrorService.Instance.HasWarnings(), true);
            ClearState();
        }

        [TestMethod]
        public void TestResolveWarning()
        {
            int? testId = null;
            testId = ErrorService.Instance.ReportWarning(0, new List<string>() { "Valid Words", "Banned Words" }, testId);
            ErrorService.Instance.ResolveWarning(0, testId);
            Assert.AreEqual(ErrorService.Instance.HasWarnings(), false);
            ClearState();
        }
    }
}
