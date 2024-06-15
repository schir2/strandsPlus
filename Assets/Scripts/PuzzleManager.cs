using System;
using System.Collections.Generic;
using UnityEngine;

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
        TextAsset jsonFile = Resources.Load<TextAsset>(dailyPuzzleFileName.Replace(".json", ""));
        if (jsonFile != null)
        {
            string json = jsonFile.text;
            dailyPuzzles = JsonUtility.FromJson<Dictionary<string, Puzzle>>(json);
            Debug.Log(dailyPuzzles);
        }
        else
        {
            Debug.LogError("Resource file not found!");
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



    [Serializable]
    private class Wrapper
    {
        public Dictionary<string, Puzzle> dailyPuzzles;
    }
}