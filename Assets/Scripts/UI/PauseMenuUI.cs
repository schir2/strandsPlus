using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        public Button resumeButton;

        public Button mainMenuButton;

        private void Start()
        {
            resumeButton.onClick.AddListener(OnPauseButtonClicked);
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private static void OnMainMenuButtonClicked()
        {
            GameManager.Instance.ChangeState(GameState.MainMenu);
        }

        private static void OnPauseButtonClicked()
        {
            GameManager.Instance.ChangeState(GameState.Playing);
        }
    }
}