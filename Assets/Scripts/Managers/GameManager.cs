using System;
using Data;
using Gameplay;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public GameState CurrentGameState { get; private set; }
        public GameObject mainMenuPanel;
        public GameObject gameModeSettingsPanel;
        public GameObject gamePausedPanel;
        public GameObject gameWonPanel;
        public GameObject gameLostPanel;
        public GameObject playingPanel;
        public Board board;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            ChangeState(GameState.MainMenu);
        }

        public void ChangeState(GameState newState)
        {
            CurrentGameState = newState;

            mainMenuPanel.SetActive(newState == GameState.MainMenu);
            gameModeSettingsPanel.SetActive(newState == GameState.GameModeSettings);
            gamePausedPanel.SetActive(newState == GameState.Paused);
            gameWonPanel.SetActive(newState == GameState.Won);
            gameLostPanel.SetActive(newState == GameState.Lost);
            playingPanel.SetActive(newState == GameState.Playing);

            switch (newState)
            {
                case GameState.MainMenu:
                    // Handle MainMenu state
                    break;
                case GameState.Playing:
                    // Handle Playing state
                    break;
                case GameState.Paused:
                    // Handle Paused state
                    break;
                case GameState.Lost:
                    // Handle GameOver state
                    break;
                case GameState.Won:
                    // Handle Victory state
                    break;
            }
        }

        internal void OnSelectDailyPuzzle()
        {
            GameModeManager.Instance.ApplyDefaultGameMode();
            PuzzleManager.Instance.GetTodaysPuzzle();
            board.InitializeBoard(PuzzleManager.Instance.currentPuzzle);
            ChangeState(GameState.Playing);
        }

        internal void OnStartGame()
        {
            throw new NotImplementedException();
        }

        internal void OnGameModeSettings()
        {
            throw new NotImplementedException();
        }

        private void StartPuzzle(Puzzle puzzle)
        {
            if (puzzle == null)
            {
                Debug.LogError("No puzzle loaded.");
                return;
            }

            // Update the board with the current puzzle
            board.InitializeBoard(puzzle);
            Debug.Log($"Starting puzzle: {puzzle.data.theme}");
        }
    }
}