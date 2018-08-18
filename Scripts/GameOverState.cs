using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class GameOverState : MonoBehaviour
{
    public GameObject player;
    public GameObject farmers;
    public GameStateManager GSM;
    public Animator GamePlayUI;
    public ScreenManager screenManager;

    public void StartNewGame()
    {
        foreach (Transform child in farmers.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        player.transform.position = Vector3.zero;
        GSM.SetGameState(GameState.GAMEPLAY);
        screenManager.OpenPanel(GamePlayUI);
    }
}
