using System.Collections.Generic;
using LastLetterGameDotNet.Users;
using LastLetterGameDotNet.Validator;

namespace LastLetterGameDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var user1 = new User("User1");
            var user2 = new User("User2");//new BotUser(new HashSet<string> (new DictionaryValidator().Words));
            var game = new Game(user1,user2);
            game.StartChallenge();
        }
    }
}
