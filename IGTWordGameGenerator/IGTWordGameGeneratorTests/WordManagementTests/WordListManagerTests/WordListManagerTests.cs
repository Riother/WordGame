using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IGT.WordGameGenerator.Services.WordManagement;
using IGT.WordGameGenerator.Services.WordManagement.WordListManager;

namespace IGTWordGameGeneratorTests.WordManagementTests.WordListManagerTests
{
    [TestClass]
    public class WordListManagerTests
    {
        private string acceptedWordsPath = "../../testLists/WinningWords.txt";
        private string bannedWordsPath = "../../testLists/BannedWords.txt";
        private string complexityWordsPath = "../../testLists/ComplexityWords.txt";

        [TestMethod]
        public void TestListManagerConstructor()
        {
            // verifies that files exists
            WordListManager listManager = new WordListManager(acceptedWordsPath, bannedWordsPath, 3, 5);
        }

        [TestMethod]
        public void TestGetAcceptedWords()
        {
            WordListManager listManager = new WordListManager(acceptedWordsPath, bannedWordsPath, 3, 8);
            Word test1 = new Word("ABANDON", 10);
            int complexity1 = test1.GetComplexity();
            Word test2 = new Word("ZOOM", 10);
            int complexity2 = test2.GetComplexity();
            List<string>[] acceptedWords = listManager.GetAcceptedWords();
            Assert.IsTrue(acceptedWords[complexity1].Contains("ABANDON"));
            Assert.IsTrue(acceptedWords[complexity2].Contains("ZOOM"));
        }

        [TestMethod]
        public void TestGetBannedWords()
        {
            WordListManager listManager = new WordListManager(acceptedWordsPath, bannedWordsPath, 3, 8);
            List<string> bannedWords = listManager.GetBannedWords();
            Assert.AreEqual(bannedWords[0], "ASS");
            Assert.AreEqual(bannedWords[bannedWords.Count - 1], "QUICKIE");
        }

        [TestMethod]
        public void TestGetMostComplexWord()
        {
            WordListManager listManager = new WordListManager(complexityWordsPath, bannedWordsPath, 3, 7);
            Assert.AreEqual(listManager.GetMostComplexCount(), 7);
        }

        [TestMethod]
        public void TestGetLeastComplexWord()
        {
            WordListManager listManager = new WordListManager(complexityWordsPath, bannedWordsPath, 3, 7);
            Assert.AreEqual(listManager.GetLeastComplexCount(), 3);
        }
    }
}
