namespace LastLetterGameDotNet.Validator
{
    public class GameValidator : IValidator
    {
        public bool Validate(string word) => word.Split(' ').Length == 1;
    }
}
