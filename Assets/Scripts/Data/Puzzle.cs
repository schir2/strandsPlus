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

        public PuzzleData data;
        public HashSet<string> validWords;
        public PuzzleState state = new PuzzleState();

        public void Init(PuzzleData data, HashSet<string> validWords)
        {
            this.data = data;
            this.validWords = validWords;
        }

        public bool IsSpangramWord(string word)
        {
            return word == data.spangram;
        }

        public bool IsCorrectdWord(string word)
        {
            return data.correctWords.Contains(word);
        }

        public bool IsValidWord(string word)
        {
            return validWords.Contains(word);
        }

        public GuessResult Guess(string word)
        {
            state.lastWordGuessed = word;

            if (IsSpangramWord(word))
            {
                state.spangramFound = true;
                state.lastGuessResult = GuessResult.Spangram;
                state.IncrementCorrectGuessCount();
            }
            else if (IsCorrectdWord(word))
            {
                state.correctWordsGuessed.Add(word);
                state.lastGuessResult = GuessResult.Correct;
                state.IncrementCorrectGuessCount();
            }
            else if (IsValidWord(word))
            {
                state.lastGuessResult = GuessResult.Valid;
                if (!state.wordsGuessed.Contains(word))
                {
                    state.IncrementCorrectGuessCount();
                }
            }
            else
            {
                state.lastGuessResult = GuessResult.Invalid;
            }

            return state.lastGuessResult;
        }

        public List<List<int>> RevealWord()
        {
            if (state.hints > 0)
            {
                foreach (string word in data.correctWords)
                {
                    if (!state.correctWordsGuessed.Contains(word))
                    {
                        state.hints--;
                        return data.wordPositions[word];
                    }
                }
            }

            return null;
        }
    }
}