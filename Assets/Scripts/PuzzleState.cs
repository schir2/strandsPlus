using System.Collections.Generic;

public class PuzzleState
{
    public int hints = 0;
    public int hintsUsed = 0;
    public int guessesUsed = 0;
    public int currentStreak = 0;
    public int longestStreak = 0;
    public int correctGuessCount = 0;
    public int elapsedTimeInSeconds = 0;
    public bool spangramFound = false;
    public string lastWordGuessed;
    public bool solved = false;

    public Puzzle.GuessResult lastGuessResult;

    public HashSet<string> validWordsGuessed = new HashSet<string>();
    public HashSet<string> correctWordsGuessed = new HashSet<string>();
    public HashSet<string> wordsGuessed = new HashSet<string>();

    public void IncrementCorrectGuessCount()
    {
        correctGuessCount++;
    }

    public int PuzzleWordsFound()
    {
        return correctWordsGuessed.Count + (spangramFound ? 1 : 0);
    }

}