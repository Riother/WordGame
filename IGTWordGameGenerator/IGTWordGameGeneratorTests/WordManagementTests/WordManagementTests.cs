using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IGT.WordGameGenerator.Services.WordManagement;

namespace IGTWordGameGeneratorTests.WordManagementTests
{
    [TestClass]
    public class WordManagementTests
    {
        private string acceptedWordsPath = "../../testLists/WinningWords.txt";
        private string bannedWordsPath = "../../testLists/BannedWords.txt";

        private string containsBannedWordPath = "../../testLists/HasBannedWord.txt";

        [TestMethod]
        public void TestWordManagerConstructor()
        {
            WordManager manager = new WordManager(5, new List<int>(){3,4,5,6,7}, acceptedWordsPath, bannedWordsPath);
            Word w = manager.GetWordOfComplexity(4);
            Assert.AreEqual(manager.GetWordOfComplexity(4), w);            
        }

        [TestMethod]
        public void TestGetLongestLength()
        {
            WordManager manager = new WordManager(5, new List<int>() { 3, 4, 5, 6, 7 }, acceptedWordsPath, bannedWordsPath);
            Assert.AreEqual(manager.GetWordOfComplexity(7).GetComplexity(), manager.GetLongestLength());
        }

        [TestMethod]
        public void TestContainsBannedWords()
        {
            WordManager manager = new WordManager(3, new List<int>() { 4, 5, 6 }, containsBannedWordPath, bannedWordsPath);
            Word before = manager.GetWordOfComplexity(4);
            while (!before.Equals(new Word("ASSET", 6)))
            {
                manager = new WordManager(3, new List<int>() { 4, 5, 6 }, containsBannedWordPath, bannedWordsPath);
                before = manager.GetWordOfComplexity(4);
            }
            manager.HandleBannedWords();
            Assert.AreNotEqual(manager.GetWordOfComplexity(4), new Word("ASSET", 5));
        }
    }
}
