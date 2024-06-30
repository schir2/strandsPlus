using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class PuzzleState
    {
        public HashSet<string> validWordsGuessed = new();
        public int Hints = 0;
        public int GuessCount = 0;
        private int correctGuessCount = 0;
        public HashSet<string> correctWordsGuessed = new();
        public HashSet<string> wordsGuessed = new();
        public bool spangramFound = false;
        public string lastWordGuessed;
        public Puzzle.GuessResult lastGuessResult;
        public int hintsUsedCount = 0;
        public int longestStreak = 0;
        public int currentStreak = 0;
        public int spangramFoundIn = int.MaxValue;

        public int PuzzleWordsFound()
        {
            return correctWordsGuessed.Count + (spangramFound ? 1 : 0);
        }

        public void UpdateState(Puzzle.GuessResult guessResult, string word)
        {
            lastWordGuessed = word;
            lastGuessResult = guessResult;

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
                    correctWordsGuessed.Add(word);
                    currentStreak++;
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
            wordsGuessed.Add(word);
        }
    }
}