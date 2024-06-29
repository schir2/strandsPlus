using Data;
using Managers;

namespace GameModes
{
    public class TimeAttackGameMode : GameMode
    {
        public TimeAttackGameMode() : base("Time Attack", GameModeOptions.TimeTrial)
        {
        }

        public override bool CheckWinCondition(Puzzle puzzle)
        {
            return puzzle.Data.correctWords.Count == puzzle.State.correctWordsGuessed.Count &&
                   puzzle.State.spangramFound;
        }

        public override bool CheckLoseCondition(Puzzle puzzle)
        {
            // return puzzle.State.elapsedTime >= puzzle.State.timeLimit;
            return false;
        }
    }
}