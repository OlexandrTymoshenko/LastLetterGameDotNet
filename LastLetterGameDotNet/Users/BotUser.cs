using LastLetterGameDotNet.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LastLetterGameDotNet.Users
{
    public class BotUser : IUser
    {
        private readonly IEnumerable<string> _wordsDictionary;
        private string _lastWord;
        private Random _rnd = new Random();
        public BotUser(ISet<string> wordsDictionary)
        {
            _wordsDictionary = wordsDictionary;
        }

        public string Name { get; set; } = "Bot";

        public UserConfirmationAction GetConfirmationAction(string lastWord) {
            _lastWord = lastWord;
            return UserConfirmationAction.Accept;
        }

        public string GetInputtedWord(string lastWord)
        {
            int month = _rnd.Next();
            var answers = _wordsDictionary.Where(x => x.StartsWith(_lastWord[_lastWord.Length - 1])).ToList();
            return answers[_rnd.Next(answers.Count-1)];
        }

        public void ShowValidationErrorMessage(string msg) {}
    }
}