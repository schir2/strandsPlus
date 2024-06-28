using System;

namespace Managers
{
    [Serializable]
    public class GameMode
    {
        public string name;
        public GameModeOptions options;

        public GameMode(string name, GameModeOptions options)
        {
            this.name = name;
            this.options = options;
        }
    }
}