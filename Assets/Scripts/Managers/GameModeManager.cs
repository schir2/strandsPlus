using System;
using System.Collections.Generic;
using GameModes;
using UnityEngine;

namespace Managers
{
    public class GameModeManager : MonoBehaviour
    {
        public static GameModeManager Instance { get; private set; }

        public GameMode CurrentGameMode { get; private set; }
        private Dictionary<string, GameMode> gameModes;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeGameModes();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeGameModes()
        {
            gameModes = new Dictionary<string, GameMode>
            {
                { "Standard", new StandardGameMode() },
            };

            CurrentGameMode = gameModes["Standard"];
        }

        public void SetGameMode(string modeName)
        {
            if (gameModes.ContainsKey(modeName))
            {
                CurrentGameMode = gameModes[modeName];
                Debug.Log($"Game mode changed to: {CurrentGameMode.name}");
            }
            else
            {
                Debug.LogError($"Game mode {modeName} does not exist.");
            }
        }

        public void SetCustomGameMode(GameModeOptions options)
        {
            throw new NotImplementedException();
        }

        public void ApplyDefaultGameMode()
        {
            CurrentGameMode = gameModes["Standard"];
            Debug.Log("Standard game mode applied.");
        }
    }
}