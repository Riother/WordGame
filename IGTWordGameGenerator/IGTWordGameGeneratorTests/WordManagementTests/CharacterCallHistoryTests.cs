using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IGT.WordGameGenerator.Services.WordManagement;

namespace IGTWordGameGeneratorTests.WordManagementTests
{
    [TestClass]
    public class CharacterCallHistoryTests
    {
        private string acceptedWordsPath = "../../testLists/WinningWords.txt";
        private string bannedWordsPath = "../../testLists/BannedWords.txt";

        [TestMethod]
        public void VerifyHorizontalCallLettersFix()
        {
            int callLettersPerRow = 4;
            List<char> characters = new List<char>()
            {
                'A', 'S', 'S', 'T',
                'B', 'I', 'L', 'K',
                'J', 'F', 'O',
            };

            WordManager manager = new WordManager(4, new List<int>() { 3, 4, 5, 6 }, acceptedWordsPath, bannedWordsPath);
            manager.RemoveCallHistoryBannedWords(characters, callLettersPerRow);
            bool results = characters[0] != 'a' || characters[1] != 's' || characters[2] != 's' || characters[3] != 't';
            Assert.AreEqual(results, true);
        }

        [TestMethod]
        public void VerifyVerticalCallLettersFix()
        {
            int callLettersPerRow = 4;
            List<char> characters = new List<char>()
            {
                'A', 'B', 'S', 'T',
                'S', 'I', 'L', 'K',
                'S', 'F', 'O',
            };

            WordManager manager = new WordManager(4, new List<int>() { 3, 4, 5, 6 }, acceptedWordsPath, bannedWordsPath);
            manager.RemoveCallHistoryBannedWords(characters, callLettersPerRow);
            bool results = characters[0] != 'a' || characters[4] != 's' || characters[8] != 's';
            Assert.AreEqual(results, true);
        }
    }
}
