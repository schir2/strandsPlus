using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    Lost,
    Won
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentGameState { get; private set; }

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
}
