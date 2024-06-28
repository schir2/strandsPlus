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

        public bool IsCorrectdWord(string word)
        {
            return Data.correctWords.Contains(word);
        }

        public bool IsValidWord(string word)
        {
            return ValidWords.Contains(word);
        }

        public GuessResult Guess(string word)
        {
            State.lastWordGuessed = word;

            if (IsSpangramWord(word))
            {
                State.spangramFound = true;
                State.lastGuessResult = GuessResult.Spangram;
                State.IncrementCorrectGuessCount();
            }
            else if (IsCorrectdWord(word))
            {
                State.correctWordsGuessed.Add(word);
                State.lastGuessResult = GuessResult.Correct;
                State.IncrementCorrectGuessCount();
            }
            else if (IsValidWord(word))
            {
                State.lastGuessResult = GuessResult.Valid;
                if (!State.wordsGuessed.Contains(word))
                {
                    State.IncrementCorrectGuessCount();
                }
            }
            else
            {
                State.lastGuessResult = GuessResult.Invalid;
            }

            return State.lastGuessResult;
        }

        public List<List<int>> RevealWord()
        {
            if (State.hints > 0)
            {
                foreach (string word in Data.correctWords)
                {
                    if (!State.correctWordsGuessed.Contains(word))
                    {
                        State.hints--;
                        return Data.wordPositions[word];
                    }
                }
            }

            return null;
        }
    }
}