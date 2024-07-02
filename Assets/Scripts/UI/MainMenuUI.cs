using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public Button dailyGameButton;
        public Button randomGameButton;
        public Button gameModeSettingsButton;
        public Button exitGameButton;
        public Button resumeGameButton;

        private void Start()
        {
            dailyGameButton.onClick.AddListener(OnDailyGameButtonClicked);
            randomGameButton.onClick.AddListener(OnRandomGameButtonClicked);
            gameModeSettingsButton.onClick.AddListener(OnGameModeSettingsButtonClicked);
            exitGameButton.onClick.AddListener(OnExitGameButtonClicked);
            resumeGameButton.onClick.AddListener(OnResumeGameButtonClicked);
        }

        private static void OnResumeGameButtonClicked()
        {
            GameManager.Instance.ChangeState(GameState.Playing);
        }

        private static void OnDailyGameButtonClicked()
        {
            GameManager.Instance.StartDailyPuzzle();
        }

        private static void OnRandomGameButtonClicked()
        {
            GameManager.Instance.StartRandomPuzzle();
        }

        private static void OnGameModeSettingsButtonClicked()
        {
            GameManager.Instance.ChangeState(GameState.GameModeSettings);
        }

        private static void OnExitGameButtonClicked()
        {
            Application.Quit();
        }
        private void Update()
        {
            resumeGameButton.gameObject.SetActive(PuzzleManager.Instance.ActivePuzzle != null);
        }
    }
}