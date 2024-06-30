using System;
using System.Collections.Generic;
using Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Managers
{
    public class PuzzleManager : MonoBehaviour
    {
        public static PuzzleManager Instance { get; private set; }

        private Dictionary<string, PuzzleData> dailyPuzzles;
        private readonly HashSet<string> validWords = new();
        public string dailyPuzzleFileName = "dailyPuzzles.json";
        public Puzzle ActivePuzzle { get; private set; }


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                LoadValidWords();
                LoadDailyPuzzles();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void LoadDailyPuzzles()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>(dailyPuzzleFileName.Replace(".json", ""));
            if (jsonFile != null)
            {
                var json = jsonFile.text;
                dailyPuzzles = JsonConvert.DeserializeObject<Dictionary<string, PuzzleData>>(json);
            }
            else
            {
                Debug.LogError("Resource file not found!");
            }
        }

        void LoadValidWords()
        {
            var wordListTextAsset = Resources.Load<TextAsset>("dictionary");

            if (wordListTextAsset != null)
            {
                var words =
                    wordListTextAsset.text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in words)
                {
                    validWords.Add(word.Trim().ToLower());
                }
            }
            else
            {
                Debug.LogError("Failed to load valid word list!");
            }
        }

        public Puzzle GetTodaysPuzzle()
        {
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            PuzzleData data;

            if (dailyPuzzles.ContainsKey(today))
            {
                data = dailyPuzzles[today];
            }
            else
            {
                data = dailyPuzzles["2024-06-16"];
            }

            ActivePuzzle = new Puzzle(data, validWords);
            return ActivePuzzle;
        }
    }
}