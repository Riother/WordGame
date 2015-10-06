using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGT.WordGameGenerator.Services.WordManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IGTWordGameGeneratorTests.WordManagementTests
{
    [TestClass]
    public class WordTests
    {
        [TestMethod]
        public void TestGetLetter()
        {
            Word w = new Word("test", 4);
            Assert.AreEqual(w.GetLetterAt(0), 't');
            Assert.AreEqual(w.GetLetterAt(3), 't');
            Assert.AreNotEqual(w.GetLetterAt(2), 'e');
        }

        [TestMethod]
        public void TestContains()
        {
            Word w = new Word("hello", 5);
            Assert.AreEqual(w.Contains("hell"), true);
        }

        [TestMethod]
        public void TestNotContains()
        {
            Word w = new Word("hello", 5);
            Assert.AreEqual(w.Contains("test"), false);
        }

        [TestMethod]
        public void TestEquals()
        {
            Word one = new Word("test", 4);
            Word two = new Word("test", 4);
            Assert.AreEqual(one, two);
        }

        [TestMethod]
        public void TestNotEquals()
        {
            Word one = new Word("test", 5);
            Word two = new Word("test", 4);
            Assert.AreNotEqual(one, two);
        }

        [TestMethod]
        public void TestHashCode()
        {
            int desiredHash = 't'.GetHashCode() + 'e'.GetHashCode() + 's'.GetHashCode() + 't'.GetHashCode();
            Word w = new Word("test", 4);
            Assert.AreEqual(desiredHash, w.GetHashCode());
        }
    }
}
