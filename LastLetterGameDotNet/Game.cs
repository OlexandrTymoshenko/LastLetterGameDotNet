using System;
using System.Collections.Generic;
using LastLetterGameDotNet.Enums;
using LastLetterGameDotNet.Users;
using NLog;

namespace LastLetterGameDotNet
{
    public class Game
    {
        private readonly State _gameState;
        private readonly Validator.Validator _validator;
        private readonly IDictionary<UserConfirmationAction,Action> _confirmationActions;
        private IUser ActiveUser => _gameState.ActiveUser;
        private readonly ILogger _logger;

        public Game(IUser user1, IUser user2)
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
            _gameState = State.LoadState();
            _gameState = _gameState ?? new State(user1,user2);
            _validator = new Validator.Validator(_gameState);
            _confirmationActions = new Dictionary<UserConfirmationAction, Action>
            {
                {UserConfirmationAction.Accept, Accept},
                {UserConfirmationAction.Reject, Reject},
            };
            var gameActions = new Dictionary<GameAction, Action>
            {
                {GameAction.Confirmation, Notify},
                {GameAction.WordInputting, StartChallenge},
            };
            if (_gameState.CurrentGameAction != null)
                gameActions[_gameState.CurrentGameAction.Value]();
        }

        public void Notify()
        {
            _gameState.CurrentGameAction = GameAction.Confirmation;
            State.SaveState(_gameState);

            if (_gameState.IsValidInput)
            {
                ToggleActiveUser();
                var confirmationAction = ActiveUser.GetConfirmationAction(_gameState.LastTempWord);
                _logger.Info($"User '{ActiveUser.Name}' '{confirmationAction}' word '{_gameState.LastTempWord}'");
                _confirmationActions[confirmationAction]();
            }
            else
            {
                ActiveUser.ShowValidationErrorMessage("validation error");
                _logger.Info($"User '{ActiveUser.Name}' received validation error");
                StartChallenge();
            }
        }

        public void Accept()
        {
            _gameState.IsFirstChallange = false;
            _gameState.LastWord = _gameState.LastTempWord;
            _gameState.PreviousWords.Add(_gameState.LastWord);
            StartChallenge();
        }

        public void Reject()
        {
            ToggleActiveUser();
            StartChallenge();
        }

        public void StartChallenge()
        {
            _logger.Info($"Requesting word from User '{_gameState.ActiveUser}'");
            _gameState.CurrentGameAction = GameAction.WordInputting;
            State.SaveState(_gameState);

            var word = ActiveUser.GetInputtedWord(_gameState.LastWord ?? string.Empty);
            _logger.Info($"User '{ActiveUser.Name}' inputted '{word}'");
            var isValid = _validator.Validate(word);
            _gameState.IsValidInput = isValid;
            _gameState.LastTempWord = word;
            Notify();
        }

        private void ToggleActiveUser () => _gameState.ActiveUser = ActiveUser.Name == _gameState.User1.Name ? _gameState.User2 : _gameState.User1;
        
    }
}
