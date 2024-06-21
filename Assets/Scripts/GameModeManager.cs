using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance { get; private set; }

    public GameMode currentGameMode;
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
            { "Standard", new GameMode("Standard", GameModeOptions.None) },
            { "TimeAttack", new GameMode("Time Attack", GameModeOptions.TimeTrial) },
            { "AllOptions", new GameMode("All Options", GameModeOptions.TimeTrial | GameModeOptions.Timed | GameModeOptions.Hints | GameModeOptions.OnlyCorrectWords | GameModeOptions.SpangramOnly) },
            { "Custom", new GameMode("Custom", GameModeOptions.None) } // Initially empty, user will set options
        };

        currentGameMode = gameModes["Standard"];
    }

    public void SetGameMode(string modeName)
    {
        if (gameModes.ContainsKey(modeName))
        {
            currentGameMode = gameModes[modeName];
            Debug.Log($"Game mode changed to: {currentGameMode.name}");
        }
        else
        {
            Debug.LogError($"Game mode {modeName} does not exist.");
        }
    }

    public void SetCustomGameMode(GameModeOptions options)
    {
        currentGameMode = new GameMode("Custom", options);
        Debug.Log("Custom game mode set.");
    }

    public void ApplyDefaultGameMode()
    {
        currentGameMode = gameModes["Standard"];
        Debug.Log("Standard game mode applied.");
    }
}
