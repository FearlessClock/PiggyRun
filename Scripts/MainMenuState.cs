using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : MonoBehaviour, State {
    public GameStateManager GSM;
    public ScreenManager screenManager;
    public Animator gameplayUI;
    
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

    public void OnTransition()
    {
        throw new System.NotImplementedException();
    }

    public void StateUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void HandleStateChange()
    {
        Debug.Log("MainMenuScreen");
    }
}
