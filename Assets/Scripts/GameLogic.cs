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


    private HashSet<string> correctWords = new HashSet<string>();
    private HashSet<string> validWords = new HashSet<string>();
    private string spangram;
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
            { 'a', 's', 's', 'e', 'p', 's' }
        };
    }

    public bool IsCorrectdWord(string word)
    {
        return correctWords.Contains(word);
    }

    private bool IsValidWord(string word)
    {
        return validWords.Contains(word);
    }

    public WordEvaluation EvaluateWord(string word)
    {
        if (word == spangram)
        {
            return WordEvaluation.Spangram;
        }
        else if (IsCorrectdWord(word))
        {
            return WordEvaluation.Correct;
        }
        else if (IsValidWord(word))
        {
            return WordEvaluation.Valid;
        }
        return WordEvaluation.Invalid;
    }

    private void Awake()
    {
        // Load the text file containing the dictionary of valid words from Resources folder
        TextAsset wordListTextAsset = Resources.Load<TextAsset>("dictionary");

        if (wordListTextAsset != null)
        {
            // Parse the loaded text to extract valid words
            string[] words = wordListTextAsset.text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

            // Add the valid words to the HashSet
            foreach (string word in words)
            {
                validWords.Add(word.Trim().ToLower()); // Trim whitespace and convert to lowercase for consistency
            }
        }
        else
        {
            Debug.LogError("Failed to load valid word list!");
        }
    }
}

