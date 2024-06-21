using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Puzzle
{
    public enum WordEvaluation
    {
        Spangram,
        Correct,
        Valid,
        Invalid
    }
    public PuzzleData data;
    public PuzzleState state = new PuzzleState();

    public void Init(PuzzleData data)
    {
        this.data = data;
    }

    public int PuzzleWordsCount()
    {
        return data.correctWords.Count + 1;
    }

    public int PuzzleWordsFound()
    {
        return state.correctWordsGuessed.Count + (state.spangramFound ? 1 : 0);
    }
    public bool IsSpangramWord(string word)
    {
        return word == data.spangram;
    }

    public bool IsCorrectdWord(string word)
    {
        return data.correctWords.Contains(word);
    }

    private bool IsValidWord(string word)
    {
        return PuzzleManager.Instance.validWords.Contains(word);
    }

    private void IncrementCorrectGuessCount()
    {
        state.correctGuessCount++;
    }

    public WordEvaluation Guess(string word)
    {
        state.lastWordGuessed = word;
        if (IsSpangramWord(word))
        {
            state.spangramFound = true;
            state.lastWordEvaluation = WordEvaluation.Spangram;
            IncrementCorrectGuessCount();
        }
        else if (IsCorrectdWord(word))
        {
            state.correctWordsGuessed.Add(word);
            state.lastWordEvaluation = WordEvaluation.Correct;
            IncrementCorrectGuessCount();
        }
        else if (IsValidWord(word))
        {
            state.lastWordEvaluation = WordEvaluation.Valid;
            if (!state.wordsGuessed.Contains(word))
            {
                IncrementCorrectGuessCount();
            }
        }
        else
        {
            state.lastWordEvaluation = WordEvaluation.Invalid;
        }
        return state.lastWordEvaluation;
    }
}

public class PuzzleData
{
    public string theme;
    public string spangram;
    public List<string> correctWords;
    public Dictionary<string, List<List<int>>> wordPositions;
    public List<List<string>> puzzleGrid;
}

public class PuzzleState
{
    public HashSet<string> validWordsGuessed = new HashSet<string>();
    public int hints = 0;
    public int correctGuessCount = 0;
    public HashSet<string> correctWordsGuessed = new HashSet<string>();
    public HashSet<string> wordsGuessed = new HashSet<string>();
    public bool spangramFound = false;
    public string lastWordGuessed;
    public Puzzle.WordEvaluation lastWordEvaluation;

}