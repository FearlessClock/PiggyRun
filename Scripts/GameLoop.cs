using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour {

    public GameStateManager GSM;
    public ScreenManager screenManager;
    public Animator FadeToBlackMenu;
    private RawImage rawImage;

    public Animator gameplayMenu;
    bool isRunning = false;
    float timer = 0;
	// Use this for initialization
	void Start () {
        rawImage = FadeToBlackMenu.transform.GetChild(0).gameObject.GetComponent<RawImage>();
    }
	
	// Update is called once per frame
	void Update () {
        if(GSM.gameState == GameState.MAIN_MENU)
        {

        }
        else if(GSM.gameState == GameState.RESTART)
        {
            if(isRunning == false)
            {
                screenManager.OpenPanel(FadeToBlackMenu);
                isRunning = true;
                timer = 0;
            }
            timer += Time.deltaTime;
            if(timer > 2)
            {
                isRunning = false;
                timer = 0;
                GSM.SetGameState(GameState.GAMEPLAY);
                screenManager.OpenPanel(gameplayMenu);
            }
            else
            {
                rawImage.color = new Color(0, 0, 0, (-timer * timer + 2*timer));
                if(rawImage.color.a < 0.1)
                {
                    rawImage.color = new Color(0,0,0,0);
                }
            }
        }
	}
}
