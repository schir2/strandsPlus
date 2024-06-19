using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button dailyGameButton;
    public Button randomGameButton;
    public Button gameModeSettingsButton;
    public Button exitGameButton;

    private void Start()
    {
        dailyGameButton.onClick.AddListener(OnDailyGameButtonClicked);
        randomGameButton.onClick.AddListener(OnRandomGameButtonClicked);
        gameModeSettingsButton.onClick.AddListener(OnGameModeSettingsButtonClicked);
        exitGameButton.onClick.AddListener(OnExitGameButtonClicked);
    }

    private void OnDailyGameButtonClicked()
    {
        GameManager.Instance.OnSelectDailyPuzzle();
    }

    private void OnRandomGameButtonClicked()
    {
        // Implement logic for random game if necessary
        GameManager.Instance.OnStartGame(); // This is just a placeholder, adjust as needed
    }

    private void OnGameModeSettingsButtonClicked()
    {
        GameManager.Instance.OnGameModeSettings();
    }

    private void OnExitGameButtonClicked()
    {
        Application.Quit();
    }
}
