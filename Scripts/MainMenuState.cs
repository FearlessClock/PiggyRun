using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : MonoBehaviour {
    public GameStateManager GSM;
    public ScreenManager screenManager;
    public Animator gameplayUI;
    public Animator mainMenuUI;
    public Animator shoppeUI;
    
    // Use this for initialization
    void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
    }
    
    public void StartGame()
    {
        GSM.SetGameState(GameState.STARTING);
        screenManager.CloseCurrent();
    }

    public void OpenShoppe()
    {
        GSM.SetGameState(GameState.SHOPPE);
        screenManager.OpenPanel(shoppeUI);
    }

    public void OpenMainMenu()
    {
        GSM.SetGameState(GameState.MAIN_MENU);
        screenManager.OpenPanel(mainMenuUI);
    }
}
