using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CustomGameModeUI : MonoBehaviour
    {
        public Toggle timeTrialToggle;
        public Toggle HintsToggle;
        public Toggle onlyCorrectWordsToggle;
        public Button StartCustomGameButton;

        private void Start()
        {
            StartCustomGameButton.onClick.AddListener(OnStartCustomGameButton);
        }

        private void OnStartCustomGameButton()
        {
            ApplyCustomGameMode();
            GameManager.Instance.StartCustomGame();
        }

        private void ApplyCustomGameMode()
        {
            var options = GameModeOptions.None;

            if (timeTrialToggle.isOn) options |= GameModeOptions.TimeTrial;
            if (HintsToggle.isOn) options |= GameModeOptions.Hints;
            if (onlyCorrectWordsToggle.isOn) options |= GameModeOptions.OnlyCorrectWords;

            GameModeManager.Instance.SetCustomGameMode(options);
        }
    }
}