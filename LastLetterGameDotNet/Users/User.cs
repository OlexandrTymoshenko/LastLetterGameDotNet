using System;
using LastLetterGameDotNet.Enums;
using NLog;

namespace LastLetterGameDotNet.Users
{
    public class User : IUser
    {
        public string Name { get; set; }
        private readonly ILogger _logger;
        public User(string name)
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
            Name = name;
        }

        public UserConfirmationAction GetConfirmationAction(string lastWord)
        {
            Console.WriteLine($"user '{Name}' please cofirm word:'{lastWord}' press 1 to Accept, press 2 to Reject");
            try
            {
                return (UserConfirmationAction)int.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                _logger.Error(e,"inputed command is not number");
                throw;
            }
            catch (InvalidCastException e)
            {
                _logger.Error(e, "inputed command is not supported");
                throw;
            }

        }

        public string GetInputtedWord(string lastWord)
        {
            Console.WriteLine($"User {Name} please input word, last word was:'{lastWord}'");
            return Console.ReadLine();
        }

        public void ShowValidationErrorMessage(string msg) => Console.WriteLine(msg);
    }
}
