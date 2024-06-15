using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class Puzzle: MonoBehaviour

{
    public enum WordEvaluation
    {
        Spangram,
        Correct,
        Valid,
        Invalid
    }


    public List<string> correctWords { get; private set; } = new List<string>();
    public string theme { get; private set; }
    public string spangram { get; private set; }
    public Dictionary<string, List<Vector2Int>> wordPositions;
    public char[,] puzzleGrid;


    private HashSet<string> validWords = new HashSet<string>();
    public HashSet<string> validWordsGuessed = new HashSet<string>();
    public int hints { get; private set; } = 0;
    private int correctGuessCount = 0;
    public HashSet<string> correctWordsGuessed { get; private set; } = new HashSet<string>();
    public HashSet<string> wordsGuessed { get; private set; } = new HashSet<string>();
    public bool spangramFound { get; private set; }
    public string lastWordGuessed { get; private set; }
    public WordEvaluation lastWordEvaluation { get; private set; }

    public void Init(string theme, string spangram, List<string> correctWords, char[,] puzzleGrid, Dictionary<string, List<Vector2Int>> wordPositions)
    {
        this.theme = theme;
        this.spangram = spangram;
        this.correctWords = correctWords;
        this.puzzleGrid = puzzleGrid;
        this.wordPositions = wordPositions;
    }

    public int PuzzleWordsCount()
    {
        return correctWords.Count + 1;
    }

    public int PuzzleWordsFound()
    {
        return correctWordsGuessed.Count + (spangramFound ? 1 : 0);
    }
    public bool IsSpangramWord(string word)
    {
        return word == spangram;
    }

    public bool IsCorrectdWord(string word)
    {
        return correctWords.Contains(word);
    }

    private bool IsValidWord(string word)
    {
        return validWords.Contains(word);
    }

    private void IncrementCorrectGuessCount()
    {
        correctGuessCount++;
    }

    public WordEvaluation Guess(string word)
    {
        lastWordGuessed = word;
        if (IsSpangramWord(word))
        {
            spangramFound = true;
            lastWordEvaluation = WordEvaluation.Spangram;
            IncrementCorrectGuessCount();
        }
        else if (IsCorrectdWord(word))
        {
            correctWordsGuessed.Add(word);
            lastWordEvaluation = WordEvaluation.Correct;
            IncrementCorrectGuessCount();
        }
        else if (IsValidWord(word))
        {
            lastWordEvaluation = WordEvaluation.Valid;
            if (!wordsGuessed.Contains(word))
            {
                IncrementCorrectGuessCount();
            }
        }
        else
        {
            lastWordEvaluation = WordEvaluation.Invalid;
        }
        return lastWordEvaluation;
    }
}

[Serializable]
public class PuzzleSerializable
{
    public string theme;
    public string spangram;
    public List<string> correctWords;
    public List<List<string>> puzzleGrid;
    public Dictionary<string, List<List<int>>> wordPositions;
}