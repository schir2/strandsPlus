using Data;
using Managers;

namespace GameModes
{
    public class StandardGameMode : GameMode
    {
        public StandardGameMode() : base("Standard", GameModeOptions.None) { }

        public override bool CheckWinCondition(Puzzle puzzle)
        {
            return puzzle.Data.correctWords.Count == puzzle.State.CorrectWordsGuessed.Count && puzzle.State.spangramFound;
        }

        public override bool CheckLoseCondition(Puzzle puzzle)
        {
            return false; // Define standard lose condition if needed
        }
    }
}