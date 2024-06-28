using System.Collections.Generic;

namespace Data
{
    public class PuzzleState
    {
        public HashSet<string> validWordsGuessed = new HashSet<string>();
        public int hints = 0;
        public int guessCount = 0;
        public int correctGuessCount = 0;
        public HashSet<string> correctWordsGuessed = new HashSet<string>();
        public HashSet<string> wordsGuessed = new HashSet<string>();
        public bool spangramFound = false;
        public string lastWordGuessed;
        public Puzzle.GuessResult lastGuessResult;

        public void IncrementCorrectGuessCount()
        {
            correctGuessCount++;
        }

        public int PuzzleWordsFound()
        {
            return correctWordsGuessed.Count + (spangramFound ? 1 : 0);
        }
    }
}