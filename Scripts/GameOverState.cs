using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class GameOverState : MonoBehaviour
{
    public GameObject player;
    public GameObject farmers;
    public GameStateManager GSM;
    public ScreenManager screenManager;

    public void StartNewGame()
    {
        GSM.SetGameState(GameState.RESTART);
    }
}
