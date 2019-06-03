using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace LastLetterGameDotNet.Validator
{
    public class Validator
    {
        private readonly List<IValidator> _validators;
        private readonly ILogger _logger;

        public Validator(State state)
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
            _validators = new List<IValidator>
            {
                new GameValidator(),
                new StateValidator(state),
                new DictionaryValidator()
            };
        }

        public bool Validate(string word)
        {
            var isValid = true;

            var validationDictionary = new Dictionary<Type, bool>();
            foreach (var validator in _validators)
            {
                var validationResult = validator.Validate(word);
                validationDictionary.Add(validator.GetType(),validationResult);
                if (!validationResult)
                    isValid = false;
            }

            var validationInfo =
                validationDictionary.Select(x => $"{x.Key} : {x.Value}").Aggregate((s1, s2) => $"{s1};{s2}");
            _logger.Info($"word '{word}' validation: {validationInfo}");

            return isValid;
        }
    }
}
