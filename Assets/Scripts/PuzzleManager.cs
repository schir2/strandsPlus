using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.XR;

public class PuzzleManager : MonoBehaviour
{
    public Dictionary<string, Puzzle> dailyPuzzles;
    public string dailyPuzzleFileName = "dailyPuzzles.json";

    void Start()
    {
        LoadDailyPuzzles();
    }

    void LoadDailyPuzzles()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(dailyPuzzleFileName);
        if (jsonFile != null)
        {
            string json = jsonFile.text;
            var dailyPuzzlesData = JsonUtility.FromJson<Dictionary<string, Dictionary<string, PuzzleSerializable>>>(json);
            dailyPuzzles = new Dictionary<string, Puzzle>();

            foreach (var kvp in dailyPuzzlesData["dailyPuzzles"])
            {
                PuzzleSerializable puzzleSerializable = kvp.Value;
                Puzzle puzzle = new Puzzle();

                char[,] puzzleGrid = new char[puzzleSerializable.puzzleGrid.Count, puzzleSerializable.puzzleGrid[0].Count];
                for (int i = 0; i < puzzleSerializable.puzzleGrid.Count; i++)
                {
                    for (int j = 0; j < puzzleSerializable.puzzleGrid[i].Count; j++)
                    {
                        puzzleGrid[i, j] = puzzleSerializable.puzzleGrid[i][j][0];
                    }
                }

                Dictionary<string, List<Vector2Int>> wordPositions = new Dictionary<string, List<Vector2Int>>();
                foreach (var wordPositionsKvp in puzzleSerializable.wordPositions)
                {
                    List<Vector2Int> positions = new List<Vector2Int>();
                    foreach (var pos in wordPositionsKvp.Value)
                    {
                        positions.Add(new Vector2Int(pos[0], pos[1]));
                    }
                    wordPositions[wordPositionsKvp.Key] = positions;
                }

                puzzle.Init(puzzleSerializable.theme, puzzleSerializable.spangram, puzzleSerializable.correctWords, puzzleGrid, wordPositions);
                dailyPuzzles[kvp.Key] = puzzle;
            }
        }
        else
        {
            Debug.LogError("Local puzzle file not found!");
        }
    }

    public Puzzle GetTodaysPuzzle()
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        if (dailyPuzzles.ContainsKey(today))
        {
            return dailyPuzzles[today];
        }
        else
        {
            Debug.LogError("Puzzle for today's date not found!");
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}