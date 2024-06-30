using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameWinningScreenUI : MonoBehaviour
    {
        public TextMeshProUGUI puzzleIDValueText;
        public TextMeshProUGUI puzzleThemeValueText;
        public TextMeshProUGUI puzzleSpangramValueText;
        public TextMeshProUGUI puzzleSpangramFoundInValueText;
        public TextMeshProUGUI puzzleSpangramFoundInScoreText;
        public TextMeshProUGUI timeElapsedValueText;
        public TextMeshProUGUI timeElapsedScoreText;
        public TextMeshProUGUI guessCountValueText;
        public TextMeshProUGUI guessCountScoreText;
        public TextMeshProUGUI hintsUsedCountValueText;
        public TextMeshProUGUI hintsUsedCountScoreText;
        public TextMeshProUGUI longestStreakValueText;
        public TextMeshProUGUI longestStreakScoreText;
        public TextMeshProUGUI totalScoreText;

        public Button backButton;
        public Button shareButton;
        public Button mainMenuButton;

        private void Start()
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
            shareButton.onClick.AddListener(OnShareButtonClicked);
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private void InflateGameWinningPanel()
        {
            var puzzle = PuzzleManager.Instance.ActivePuzzle;
            if (puzzle == null) return;
            puzzleIDValueText.text = puzzle.Data.id;
            puzzleThemeValueText.text = puzzle.Data.theme;
            puzzleSpangramValueText.text = puzzle.Data.spangram;
            puzzleSpangramFoundInValueText.text = puzzle.State.spangramFoundIn.ToString();
            timeElapsedValueText.text = Timer.Instance.ElapsedTime.ToString();
            guessCountValueText.text = puzzle.State.GuessCount.ToString();
            hintsUsedCountValueText.text = puzzle.State.hintsUsedCount.ToString();
            longestStreakValueText.text = puzzle.State.longestStreak.ToString();
        }

        private void Update()
        {
            InflateGameWinningPanel();
        }

        private static void OnMainMenuButtonClicked()
        {
            GameManager.Instance.ChangeState(GameState.MainMenu);
        }

        private static void OnShareButtonClicked()
        {
            throw new NotImplementedException();
        }

        private static void OnBackButtonClicked()
        {
            throw new NotImplementedException();
        }
    }
}