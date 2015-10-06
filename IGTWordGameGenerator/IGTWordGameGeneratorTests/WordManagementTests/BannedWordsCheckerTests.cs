using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGT.WordGameGenerator.Services.WordManagement;

namespace IGTWordGameGeneratorTests.WordManagementTests
{
    [TestClass]
    public class BannedWordsCheckerTests
    {
        [TestMethod]
        public void TestBannedWordCheckerConstructor()
        {
            List<string> bannedWords = new List<string>{"test", "banned", "words"};
            BannedWordChecker checker = new BannedWordChecker(bannedWords);
        }

        [TestMethod]
        public void TestHorizontalFind()
        {
            List<Word> winningWords = new List<Word> { new Word("assets", 6), new Word("plum", 6) };
            List<string> bannedWords = new List<string> { "ass" };
            BannedWordChecker checker = new BannedWordChecker(bannedWords);
            Assert.AreEqual(checker.FindHorizontalBannedWord(winningWords), 0);
        }

        [TestMethod]
        public void TestHorizontalNoFind()
        {
            List<Word> winningWords = new List<Word> { new Word("assets", 6), new Word("plum", 6) };
            List<string> bannedWords = new List<string> { "bad" };
            BannedWordChecker checker = new BannedWordChecker(bannedWords);
            Assert.AreEqual(checker.FindHorizontalBannedWord(winningWords), -1);
        }

        [TestMethod]
        public void TestVerticalFind()
        {
            List<Word> winningWords = new List<Word>
            {
                new Word("apple", 6),
                new Word("sand", 6),
                new Word("spruce", 6)
            };
            List<string> bannedWords = new List<string> { "ass" };
            BannedWordChecker checker = new BannedWordChecker(bannedWords);
            Assert.AreEqual(checker.FindVerticalBannedWord(winningWords), 0);
        }

        [TestMethod]
        public void TestVerticalNoFind()
        {
            List<Word> winningWords = new List<Word>
            {
                new Word("spruce", 6),
                new Word("apple", 6),
                new Word("sand", 6)
            };
            List<string> bannedWords = new List<string> { "ass" };
            BannedWordChecker checker = new BannedWordChecker(bannedWords);
            Assert.AreEqual(checker.FindVerticalBannedWord(winningWords), -1);
        }

        [TestMethod]
        public void TestDiagonalFind()
        {
            List<Word> winningWords = new List<Word>
            {
                new Word("attain", 6),
                new Word("esper", 6),
                new Word("answer", 6)
            };
            List<string> bannedWords = new List<string> { "ass" };
            BannedWordChecker checker = new BannedWordChecker(bannedWords);
            Assert.AreEqual(checker.FindDiagonalBannedWord(winningWords), 0);
        }

        [TestMethod]
        public void TestDiagonalNoFind()
        {
            List<Word> winningWords = new List<Word>
            {
                new Word("esper", 6),
                new Word("attain", 6),
                new Word("answer", 6)
            };
            List<string> bannedWords = new List<string> { "ass" };
            BannedWordChecker checker = new BannedWordChecker(bannedWords);
            Assert.AreEqual(checker.FindDiagonalBannedWord(winningWords), -1);
        }
    }
}
