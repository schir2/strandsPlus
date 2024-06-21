using UnityEngine;
using UnityEngine.UI;

public class CustomGameModeUI : MonoBehaviour
{
    public Toggle timeTrialToggle;
    public Toggle HintsToggle;
    public Toggle onlyCorrectWordsToggle;

    public void ApplyCustomGameMode()
    {
        GameModeOptions options = GameModeOptions.None;

        if (timeTrialToggle.isOn) options |= GameModeOptions.TimeTrial;
        if (HintsToggle.isOn) options |= GameModeOptions.Hints;
        if (onlyCorrectWordsToggle.isOn) options |= GameModeOptions.OnlyCorrectWords;

        GameModeManager.Instance.SetCustomGameMode(options);
    }
}
