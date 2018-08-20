using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game States
public enum GameState { MAIN_MENU, GAMEPLAY, PAUSE, GAME_OVER,
    RESTART
}

public delegate void OnStateChangeHandler();

public class GameStateManager : MonoBehaviour {

    public Animator GameOverUI;
    public Animator GamePlayUI;
    public Animator MainMenuUI;

    protected GameStateManager() { }
    private static GameStateManager instance = null;
    public event OnStateChangeHandler OnStateChange;
    public GameState gameState { get; private set; }

    public static GameStateManager Instance
    {
        get
        {
            if (GameStateManager.instance == null)
            {
                DontDestroyOnLoad(GameStateManager.instance);
                GameStateManager.instance = new GameStateManager();
            }
            return GameStateManager.instance;
        }

    }

    public void SetGameState(GameState state)
    {
        //CloseUI();
        this.gameState = state;
        //SetUI();
    }

    private void SetUI()
    {
        if(gameState == GameState.GAMEPLAY)
        {
            GamePlayUI.SetBool("Open", true);
        }else if(gameState == GameState.MAIN_MENU)
        {
            MainMenuUI.SetBool("Open", true);
        }else if(gameState == GameState.GAME_OVER)
        {
            GameOverUI.SetBool("Open", true);
        }
    }

    private void CloseUI()
    {
        if (gameState == GameState.GAMEPLAY)
        {
            GamePlayUI.SetBool("Open", false);
        }
        else if (gameState == GameState.MAIN_MENU)
        {
            MainMenuUI.SetBool("Open", false);
        }
        else if (gameState == GameState.GAME_OVER)
        {
            GameOverUI.SetBool("Open", false);
        }
    }

    public void OnApplicationQuit()
    {
        GameStateManager.instance = null;
    }

}