using System.Collections.Generic;
using System.IO;
using LastLetterGameDotNet.Enums;
using LastLetterGameDotNet.Users;
using Newtonsoft.Json;

namespace LastLetterGameDotNet
{
    public class State
    {
        public string LastWord { get; set; }
        public string LastTempWord { get; set; }
        public ISet<string> PreviousWords { get; set; }
        public IUser ActiveUser { get; set; }
        public IUser User1 { get; set; }
        public IUser User2 { get; set; }
        public bool IsFirstChallange { get; set; } = true;
        public bool IsValidInput { get; set; }
        public GameAction? CurrentGameAction { get; set; }

        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.All};

        public State(IUser user1, IUser user2)
        {
            User1 = user1;
            User2 = user2;
            PreviousWords = new HashSet<string>();
            ActiveUser = user1;
        }

        public static void SaveState(State state)
        {
            var stateStr = JsonConvert.SerializeObject(state, JsonSettings);
            File.WriteAllText("./GameState.txt", stateStr);
        }

        public static State LoadState()
        {
            try
            {
                var stateStr = File.ReadAllText("./GameState.txt");
                return JsonConvert.DeserializeObject<State>(stateStr, JsonSettings);
            }
            catch (FileNotFoundException e)
            {
                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Warn(e,"State file not found");
                return null;
            }
        }
    }
}
