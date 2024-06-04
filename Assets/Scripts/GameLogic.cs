using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour

{
    public enum WordEvaluation
    {
        Correct,
        Valid,
        Invalid
    }


    private HashSet<string> correctWords = new HashSet<string>();
    private HashSet<string> validWords = new HashSet<string>();
    public char[,] GeneratePuzzle(int rows, int cols)
    {

        correctWords.Add("example");
        correctWords.Add("eggs");
        correctWords.Add("puzzle");

        // Placeholder puzzle for testing
        return new char[,]
        {
            { 's', 'h', 'i', 'p', 's', 'a' },
            { 'a', 'h', 'e', 'm', 'e', 'l' },
            { 'b', 'r', 'e', 'q', 't', 'a' },
            { 'a', 's', 'a', 'u', 'e', 'o' },
            { 'u', 's', 'k', 'f', 'i', 'h' },
            { 'a', 'g', 'e', 'a', 's', 'c' },
            { 'l', 'f', 'n', 'o', 'c', 't' },
            { 'e', 'f', 'a', 'w', 'a', 'b' }
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
        if (IsCorrectdWord(word))
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

