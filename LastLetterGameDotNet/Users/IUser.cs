using LastLetterGameDotNet.Enums;

namespace LastLetterGameDotNet.Users
{
    public interface IUser
    {
        string Name { get; set; }
        string GetInputtedWord(string lastWord);
        UserConfirmationAction GetConfirmationAction(string lastWord);
        void ShowValidationErrorMessage(string msg);
    }
}
