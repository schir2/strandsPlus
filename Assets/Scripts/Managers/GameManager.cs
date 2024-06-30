using System;
using Data;
using Gameplay;
using UI;
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
                    Timer.Instance.PauseTimer();
                    break;
                case GameState.Playing:
                    Timer.Instance.ResumeTimer();
                    break;
                case GameState.Paused:
                    Timer.Instance.PauseTimer();
                    break;
                case GameState.Lost:
                    Timer.Instance.PauseTimer();
                    break;
                case GameState.Won:
                    Timer.Instance.PauseTimer();
                    gameWonPanel.GetComponent<GameWinningScreenUI>().InflateGameWinningPanel();
                    break;
            }
        }

        internal void OnSelectDailyPuzzle()
        {
            GameModeManager.Instance.ApplyDefaultGameMode();
            PuzzleManager.Instance.GetTodaysPuzzle();
            board.InitializeBoard(PuzzleManager.Instance.ActivePuzzle);
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
            Debug.Log($"Starting puzzle: {puzzle.Data.theme}");
        }
    }
}