using System;
using Data;
using Managers;

namespace GameModes
{
    [Serializable]
    public abstract class GameMode
    {
        public string name;
        public GameModeOptions options;

        protected GameMode(string name, GameModeOptions options)
        {
            this.name = name;
            this.options = options;
        }

        public abstract bool CheckWinCondition(Puzzle puzzle);
        public abstract bool CheckLoseCondition(Puzzle puzzle);
    }
}