using System.Collections.Generic;
using System.IO;

namespace LastLetterGameDotNet.Validator
{
    public class DictionaryValidator : IValidator
    {
        public string[] Words;

        private readonly ISet<string> _dictionary;
        public DictionaryValidator()
        {
            try
            {
                Words = File.ReadAllLines("./91K nouns.txt");
            }
            catch (FileNotFoundException e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e,"dictionary file is missing");
                throw;
            }
            
            _dictionary = new HashSet<string>(Words);
        }
        
        public bool Validate(string word)=>_dictionary.Contains(word);
    }
}
