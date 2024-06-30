using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class Puzzle
    {
        public enum GuessResult
        {
            Spangram,
            Correct,
            Valid,
            Invalid
        }

        public readonly PuzzleData Data;
        public readonly HashSet<string> ValidWords;
        public PuzzleState State = new();

        public Puzzle(PuzzleData data, HashSet<string> validWords)
        {
            Data = data;
            ValidWords = validWords;
        }

        public bool IsSpangramWord(string word)
        {
            return word == Data.spangram;
        }

        public bool IsCorrectWord(string word)
        {
            return Data.correctWords.Contains(word);
        }

        public bool IsValidWord(string word)
        {
            return ValidWords.Contains(word) && !State.WordsGuessed.Contains(word);
        }

        public GuessResult Guess(string word)
        {
            var guessResult = EvaluateGuess(word);
            State.UpdateState(guessResult, word);
            return guessResult;
        }

        private GuessResult EvaluateGuess(string word)
        {
            State.lastWordGuessed = word;

            if (IsSpangramWord(word))
            {
                return GuessResult.Spangram;
            }

            if (IsCorrectWord(word))
            {
                return GuessResult.Correct;
            }

            return IsValidWord(word) ? GuessResult.Valid : GuessResult.Invalid;
        }

        public List<List<int>> RevealWord()
        {
            foreach (var word in Data.correctWords)
            {
                if (State.CorrectWordsGuessed.Contains(word)) continue;
                State.revealedWord = word;
                return Data.wordPositions[word];
            }

            return null;
        }
    }
}