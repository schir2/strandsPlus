using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour

{
    public enum WordEvaluation
    {
        Spangram,
        Correct,
        Valid,
        Invalid
    }


    public HashSet<string> correctWords { get; private set; } = new HashSet<string>();
    private HashSet<string> validWords = new HashSet<string>();
    public HashSet<string> validWordsGuessed = new HashSet<string>();
    public HashSet<string> correctWordsGuessed { get; private set; } = new HashSet<string>();
    public HashSet<string> wordsGuessed { get; private set; } = new HashSet<string>();
    public string theme { get; private set; }
    public string spangram { get; private set; }
    public bool spangramFound { get; private set; }
    public string lastWordGuessed { get; private set; }
    public WordEvaluation lastWordEvaluation { get; private set; }
    public char[,] GeneratePuzzle(int rows, int cols)
    {

        correctWords.Add("beta");
        correctWords.Add("sigma");
        correctWords.Add("gamma");
        correctWords.Add("delta");
        correctWords.Add("alpha");
        correctWords.Add("kappa");
        correctWords.Add("epsilon");
        spangram = "greekletters";
        theme = "Sorority Signs";

        // Placeholder puzzle for testing
        return new char[,]
        {
            { 't', 'g', 'r', 'a', 'l', 'p' },
            { 'a', 'e', 'e', 'e', 'k', 'h' },
            { 'b', 'a', 'm', 'm', 'l', 'a' },
            { 'd', 'g', 'a', 'e', 'k', 'a' },
            { 'e', 'l', 'a', 't', 'p', 'p' },
            { 's', 't', 'e', 't', 'a', 'l' },
            { 'i', 'g', 'r', 'n', 'o', 'i' },
            { 'a', 'm', 's', 'e', 'p', 's' }
        };
    }

    public int PuzzleWordsCount()
    {
        return correctWords.Count + 1;
    }

    public int PuzzleWordsFound()
    {
        return correctWordsGuessed.Count + (spangramFound ? 1 : 0);
    }

    public bool IsCorrectdWord(string word)
    {
        return correctWords.Contains(word);
    }

    private bool IsValidWord(string word)
    {
        return validWords.Contains(word);
    }

    public WordEvaluation Guess(string word)
    {
        lastWordGuessed = word;
        if (word == spangram)
        {
            spangramFound = true;
            lastWordEvaluation = WordEvaluation.Spangram;
        }
        else if (IsCorrectdWord(word))
        {
            correctWordsGuessed.Add(word);
            lastWordEvaluation = WordEvaluation.Correct;
        }
        else if (IsValidWord(word))
        {
            lastWordEvaluation = WordEvaluation.Valid;
        }
        else
        {
            lastWordEvaluation = WordEvaluation.Invalid;
        }
        return lastWordEvaluation;
    }

    private void Awake()
    {
        TextAsset wordListTextAsset = Resources.Load<TextAsset>("dictionary");

        if (wordListTextAsset != null)
        {
            string[] words = wordListTextAsset.text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                validWords.Add(word.Trim().ToLower());
            }
        }
        else
        {
            Debug.LogError("Failed to load valid word list!");
        }
    }
}

