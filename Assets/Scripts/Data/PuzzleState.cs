using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class PuzzleState
    {
        public int Hints = 0;
        public int GuessCount = 0;
        private int correctGuessCount = 0;
        public readonly HashSet<string> CorrectWordsGuessed = new();
        public readonly HashSet<string> WordsGuessed = new();
        public bool spangramFound = false;
        public string lastWordGuessed;
        public Puzzle.GuessResult LastGuessResult;
        public int hintsUsedCount = 0;
        public int longestStreak = 0;
        public int currentStreak = 0;
        public string revealedWord = null;
        public int spangramFoundIn = int.MaxValue;

        public int PuzzleWordsFound()
        {
            return CorrectWordsGuessed.Count + (spangramFound ? 1 : 0);
        }

        public bool UseHint()
        {
            if (revealedWord != null || Hints <= 0) return false;
            hintsUsedCount++;
            Hints--;
            return true;
        }

        public void UpdateState(Puzzle.GuessResult guessResult, string word)
        {
            lastWordGuessed = word;
            LastGuessResult = guessResult;

            switch (guessResult)
            {
                case Puzzle.GuessResult.Spangram:
                    Debug.Log($"Spangram found {word}");
                    GuessCount++;
                    spangramFound = true;
                    spangramFoundIn = GuessCount;
                    currentStreak++;
                    break;
                case Puzzle.GuessResult.Correct:
                    Debug.Log($"Correct word found {word}");
                    GuessCount++;
                    CorrectWordsGuessed.Add(word);
                    currentStreak++;
                    if (word == revealedWord)
                    {
                        revealedWord = null;
                    }

                    break;
                case Puzzle.GuessResult.Valid:
                    Debug.Log($"Valid found {word}");
                    GuessCount++;
                    correctGuessCount++;
                    currentStreak++;
                    Hints += (correctGuessCount % 3 == 0 ? 1 : 0);
                    break;
                case Puzzle.GuessResult.Invalid:
                    Debug.Log($"Invalid word found {word}");
                    currentStreak = 0;
                    break;
            }

            longestStreak = Math.Max(longestStreak, currentStreak);
            WordsGuessed.Add(word);
        }
    }
}