namespace LastLetterGameDotNet.Validator
{
    public class StateValidator : IValidator
    {
        private readonly State _state;
        public StateValidator(State state) => _state = state;
        public bool Validate(string word)
        {
            if (_state.IsFirstChallange) return true;
            return (_state.LastWord[_state.LastWord.Length - 1] == word[0])
                   && (!_state.PreviousWords.Contains(word));
        }
    }
}
